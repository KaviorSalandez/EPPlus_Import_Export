using AutoMapper;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.DTOs.Employees;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Enum;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Caches;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Core.Interfaces.Departments;
using MISA.AMISDemo.Core.Interfaces.Employees;
using MISA.AMISDemo.Core.Interfaces.Positions;
using MISA.AMISDemo.Core.Services.Base;
using MISA.AMISDemo.Core.UnitOfWorks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static MISA.AMISDemo.Core.Enum.MISAEnum;

namespace MISA.AMISDemo.Core.Services
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        IBaseRepository<Employee> _repository;
        IDepartmentRepository _departmentRepository;
        IPositionRepository _positionRepository;
        IEmployeeRepository _employeeRepository;
        ICacheService _cacheService;
        IMapper _mapper;
        IUnitOfWork _uow;

        public EmployeeService(IBaseRepository<Employee> repository, IMapper mapper, IDepartmentRepository departmentRepository, IPositionRepository positionRepository, IUnitOfWork uow, IEmployeeRepository employeeRepository, ICacheService cacheService) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _positionRepository = positionRepository;
            _uow = uow;
            _employeeRepository = employeeRepository;
            _cacheService = cacheService;
        }

        public async Task<EmployeeCountDto> FindAllFilter(int pageSize = 10, int pageNumber = 1, string search = "", string? email = "")
        {

            IEnumerable<Employee> employees = await _repository.FindAllFilter(pageSize, pageNumber, search, email);
            EmployeeCountDto result = new EmployeeCountDto();

            if (employees != null && employees.Any())
            {
                // Map entities to DTOs using AutoMapper
                var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

                // Assuming TotalRecord is a property of EmployeeCountDto
                result.Count = employees.FirstOrDefault().TotalRecord;

                result.Employees = employeeDto.ToList();
            }
            else
            {
                result.Count = 0;
                result.Employees = new List<EmployeeDto>(); // Ensure Employees list is initialized
            }

            return result;
        }

        public async Task<string> GenerateCode()
        {
            var code = await _repository.GenerateCode<Employee>();
            return code;
        }

        public override Employee MapCreateDtoToEntity(EmployeeCreateDto entity)
        {
            var entityDto = _mapper.Map<Employee>(entity);
            entityDto.EmployeeId = Guid.NewGuid();
            return entityDto;
        }

        public override EmployeeDto MapEntityToDto(Employee entity)
        {
            var entityDto = _mapper.Map<EmployeeDto>(entity);
            return entityDto;
        }


        public override Employee MapUpdateDtoToEntity(EmployeeUpdateDto updateDto, Employee entity)
        {
            updateDto.EmployeeId = entity.EmployeeId;
            var entityDto = _mapper.Map(updateDto, entity);
            return entityDto;
        }
        public override async Task CheckBeforeInsert(EmployeeCreateDto entity)
        {
            var department = await _departmentRepository.FindOne(entity.DepartmentId);
            if (department == null)
            {
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.FindDepartment);
            }
            var position = await _positionRepository.FindOne(entity.PositionId);
            if (position == null)
            {
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.FindPosition);
            }
            var employeeCode = await _employeeRepository.CheckEmployeeCode(entity.EmployeeCode.ToLower());
            if (employeeCode != null)
            {
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.EmployeeCode);
            }
        }

        public override async Task CheckBeforeUpdate(EmployeeUpdateDto entity)
        {
            var department = await _departmentRepository.FindOne(entity.DepartmentId);
            if (department == null)
            {
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.FindDepartment);
            }
            var position = await _positionRepository.FindOne(entity.PositionId);
            if (position == null)
            {
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.FindPosition);
            }
        }

        /// <summary>
        /// Chuyển đổi dữ liệu sang các bảng của excel 
        /// </summary>
        /// <typeparam name="T">kiểu thực thể T muốn chuyển đổi </typeparam>
        /// <param name="items">mảng các thực thể kiểu T </param>
        /// <returns>datatable</returns>
        public DataTable ToConvertDataTable<T>(IEnumerable<T> items, IXLWorksheet ws)
        {
            // Bảng dữ liệu 
            DataTable dt = new DataTable(typeof(T).Name);
            // Thuộc tính trong bảng của mình
            PropertyInfo[] propInfo = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            // Thêm cột số thứ tự
            dt.Columns.Add("OrdinalNumber", typeof(int));

            foreach (PropertyInfo prop in propInfo)
            {
                dt.Columns.Add(prop.Name);
            }
            int ordinalNumber = 1;
            int rowIndex = 4; 
            foreach (T item in items)
            {
                // chỉ bắt đầu từ cột số 1 
                ws.Cell(rowIndex, 1).Value = ordinalNumber; // Gán giá trị số thứ tự

                for (int i = 0; i < propInfo.Length; i++)
                {
                    var propValue = propInfo[i].GetValue(item, null);
                    if (propValue != null)
                    {
                        Type propType = propInfo[i].PropertyType;

                        // Kiểm tra nếu kiểu của thuộc tính là Nullable<>
                        if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            // Nếu là kiểu Nullable<> thì lấy kiểu bên trong
                            Type underlyingType = Nullable.GetUnderlyingType(propType);

                            if (underlyingType == typeof(DateTime))
                            {
                                // Thực hiện chuyển đổi ngày/tháng/năm ở đây
                                DateTime dateTimeValue = (DateTime)propValue;
                                ws.Cell(rowIndex, i + 2).Value = dateTimeValue.ToString("dd/MM/yyyy"); // Định dạng ngày/tháng/năm theo ý muốn
                            }
                            else
                            {
                                // Xử lý các kiểu dữ liệu Nullable<> khác nếu cần
                                ws.Cell(rowIndex, i + 2).Value = propValue?.ToString();
                            }
                        }
                        else
                        {
                            ws.Cell(rowIndex, i + 2).Value = propValue?.ToString();
                        }
                    }
                    else
                    {
                        ws.Cell(rowIndex, i + 2).Value = ""; // hoặc giá trị mặc định khác tùy thuộc vào yêu cầu của bạn
                    }
                }
                rowIndex++;
                ordinalNumber++;
            }
            return dt;
        }



        public async Task<byte[]> ExportExcel(int checkData = 1, List<Guid>? Ids = null)
        {
            IEnumerable<EmployeeExcelDto> data = new List<EmployeeExcelDto>();
            //kiểm tra xem một ICollection
            if (checkData == 1 && Ids == null)
            {
                var count = await FindAll();
                EmployeeCountDto employeeCountDto = await FindAllFilter(count.Count());
                // lấy ra rồi gán cho bên này 
                IEnumerable<EmployeeDto> data2 = employeeCountDto.Employees;
                data = data2.Select(e => _mapper.Map<EmployeeExcelDto>(e)).ToList();
            }
            else if (Ids != null && Ids.Any())
            {
                await ValidateManyIds(Ids);
                IEnumerable<Employee> data2 = await _employeeRepository.FindManyRecord(Ids);
                data = data2.Select(e => _mapper.Map<EmployeeExcelDto>(e)).ToList();
            }
            if (data.Any())
            {
                return GenerateExcelFile(data);
            }
            return null;
        }
        /// <summary>
        /// Tạo một excel file
        /// </summary>
        /// <typeparam name="T">Thực thể EmployeeExcelDto </typeparam>
        /// <param name="data">Dữ liệu export</param>
        /// <returns></returns>
        private byte[] GenerateExcelFile(IEnumerable<EmployeeExcelDto> data)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                IXLWorksheet ws = wb.Worksheets.Add(MISA.AMISDemo.Core.Resource.Resource_VN.ExcelNameEmployee);
                ToConvertDataTable<EmployeeExcelDto>(data,ws);
                string[] columnHeaders = {MISA.AMISDemo.Core.Resource.Resource_VN.OrdinalNumber,
             MISA.AMISDemo.Core.Resource.Resource_VN.EmployeeCodeVN, MISA.AMISDemo.Core.Resource.Resource_VN.Fullname, MISA.AMISDemo.Core.Resource.Resource_VN.Gender,MISA.AMISDemo.Core.Resource.Resource_VN.DOB,
             MISA.AMISDemo.Core.Resource.Resource_VN.PositionName, MISA.AMISDemo.Core.Resource.Resource_VN.DepartmentName, MISA.AMISDemo.Core.Resource.Resource_VN.AccountNumber, MISA.AMISDemo.Core.Resource.Resource_VN.BankName
                                };
                int[] columnWidths = { 7, 20, 30, 15, 15, 30, 30, 20, 20 };
                // Thêm tiêu đề cho toàn bảng (merge từ A1 đến H2)
                ws.Cell(1, 1).Value = MISA.AMISDemo.Core.Resource.Resource_VN.titleExcel;
                ws.Range("A1:I2").Merge();
                ws.Row(1).Height = 25;
                ws.Row(1).Style.Font.Bold = true;
                ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(1).Style.Font.SetFontSize(25);

                int dataStartRow = 3;
                // Thêm tiêu đề và đặt chiều rộng cho các cột
                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    ws.Cell(dataStartRow, i + 1).Value = columnHeaders[i];
                    ws.Column(i + 1).Width = columnWidths[i];
                    ws.Row(dataStartRow).Style.Font.SetFontSize(12);
                    ws.Row(dataStartRow).Style.Font.Bold = true;
                }
                ////dataStartRow++;
                //foreach (var employee in ws)
                //{
                //    int dataStartColumn = 1; // Giả sử cột bắt đầu xuất dữ liệu

                //    for (int i = 1; i < columnHeaders.Length; i++)
                //    {
                //        // Lấy tên thuộc tính tương ứng với tên cột
                //        string propertyName = columnHeaders[i].Replace(" ", ""); // Xóa khoảng trắng trong tên cột

                //        // Sử dụng reflection để lấy giá trị thuộc tính
                //        var propertyInfo = employee.GetType().GetProperty(propertyName);
                //        if (propertyInfo != null)
                //        {
                //            ws.Cell(dataStartRow, dataStartColumn).Value = Convert.ToString(propertyInfo?.GetValue(employee, null));
                //            dataStartColumn++;
                //        }
                //    }

                //    dataStartRow++; // Di chuyển sang hàng tiếp theo sau khi xử lý xong một đối tượng
                //}

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return stream.ToArray();
                }
            }

        }
        /// <summary>
        /// Kiểm tra file import 
        /// </summary>
        /// <param name="fileImport">File được import </param>
        /// <exception cref="ValidateException"></exception>
        public async Task CheckFileImport(IFormFile fileImport)
        {
            if (fileImport == null || fileImport.Length == 0)
            {
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.EmptyFile);
            }
            if (!Path.GetExtension(fileImport.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Không hợp lêj ");
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.FormatFileExcel);
            }
        }
        /// <summary>
        /// Convert ngày tháng năm 
        /// </summary>
        /// <param name="input"> chuỗi ngày tháng năm </param>
        /// <returns></returns>
        public DateTime? ProcessDate(string input)
        {
            // Regex để kiểm tra định dạng yyyy
            string yearRegex = @"^\d{4}$";

            // Regex để kiểm tra định dạng dd/MM/yyyy
            string ddMmYyRegex = @"^\d{1,2}/\d{1,2}/\d{4}$";

            // Regex để kiểm tra định dạng MM/yyyy
            string mmYyRegex = @"^\d{1,2}/\d{4}$";

            // Kiểm tra input bằng Regex
            if (Regex.IsMatch(input, yearRegex))
            {
                // Trả về ngày đầu tiên của năm được cung cấp
                return DateTime.ParseExact($"01/01/{input}", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else if (Regex.IsMatch(input, ddMmYyRegex))
            {
                // Tách chuỗi thành các phần
                string[] parts = input.Split('/');

                // Bổ sung "0" nếu phần ngày chỉ có một ký tự
                if (parts[0].Length == 1)
                {
                    parts[0] = "0" + parts[0];
                }
                if (parts[1].Length == 1)
                {
                    parts[1] = "0" + parts[1];
                }

                // Ghép chuỗi lại và định dạng thành dd/MM/yyyy
                string formattedDate = string.Join("/", parts);

                // Trả về ngày được định dạng
                return DateTime.ParseExact(formattedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            }
            else if (Regex.IsMatch(input, mmYyRegex))
            {
                string[] parts = input.Split('/');
                if (parts[0].Length == 1)
                {
                    input = "0" + input;
                }

                // Trả về ngày đầu tiên của tháng được cung cấp
                return DateTime.ParseExact($"01/{input}", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            // Nếu input không hợp lệ, trả về null
            return null;
        }

        /// <summary>
        /// Convert giới tính 
        /// </summary>
        /// <param name="gender">tên giới tính nhận được </param>
        /// <returns>Giới tính </returns>
        public Gender ConvertGender(string gender)
        {
            string male = MISA.AMISDemo.Core.Resource.Resource_VN.Male.ToLower();
            string female = MISA.AMISDemo.Core.Resource.Resource_VN.Female.ToLower();
            string other = MISA.AMISDemo.Core.Resource.Resource_VN.Other.ToLower();
            if (gender != null && gender.ToLower().Equals(male))
            {
                return MISAEnum.Gender.Nam;
            }
            if (gender != null && gender.ToLower().Equals(female))
            {
                return MISAEnum.Gender.Nữ;
            }
            if (gender != null && gender.ToLower().Equals(other))
            {
                return MISAEnum.Gender.Khác;
            }
            return MISAEnum.Gender.Nam;
        }
        // Hàm hỗ trợ thêm lỗi import vào danh sách
        private void AddImportError(EmployeeImportDto dto, string error)
        {
            dto.Errors.Add(error);
            dto.IsImported = false;
        }

        /// <summary>
        /// Tìm name trong items xem có không 
        /// </summary>
        /// <param name="items">danh sách các items </param>
        /// <param name="name">tên của giá trị muốn tìm </param>
        /// <param name="nameCompare">thuộc tính trong items muốn so sánh</param>
        /// <returns></returns>
        public object? CheckCoincidence(IEnumerable<object> items, string name, string nameCompare)
        {
            if (items == null || name == null || nameCompare == null)
            {
                return null; // Handle null inputs gracefully
            }

            // Use case-insensitive comparison (optional)
            var find = items.FirstOrDefault(item =>
            {
                // Kiểm tra xem đối tượng có thuộc tính của nameCompare không
                var nameProperty = item.GetType().GetProperty(nameCompare);
                if (nameProperty == null)
                {
                    // Không tìm thấy thuộc tính "Name", trả về false
                    return false;
                }

                // Lấy giá trị của thuộc tính "Name" và so sánh với 'name'
                var itemName = nameProperty.GetValue(item) as string;
                return itemName != null && itemName.Equals(name, StringComparison.OrdinalIgnoreCase);
            });

            return find;
        }
        public async Task<EmployeeImportParentDto> ImportExcel(IFormFile fileImport)
        {
            await CheckFileImport(fileImport);
            int countSuccess = 0, countFail = 0;
            var employeeImportDtos = new List<EmployeeImportDto>();
            var employeeImportParentDtos = new EmployeeImportParentDto();
            var employeeImportSuccess = new List<Employee>();
            var positions = await _positionRepository.FindAll();
            var departments = await _departmentRepository.FindAll();
            using (var stream = new MemoryStream())
            {
                // copy vào tệp stream 
                fileImport.CopyTo(stream);
                // thực hiện đọc dữ liệu trong file
                using (var package = new ExcelPackage(stream))
                {
                    // Đọc worksheet đầu 
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                    if (workSheet != null)
                    {
                        var rowCount = workSheet.Dimension.Rows;
                        _uow.BeginTransaction();
                        for (int row = 4; row <= rowCount; row++)
                        {
                            var employeeImportDto = new EmployeeImportDto();
                            var dob = workSheet.Cells[row, 5]?.Value?.ToString()?.Trim();
                            var gender = workSheet?.Cells[row, 4]?.Value?.ToString()?.Trim();
                            var positionName = workSheet?.Cells[row, 6]?.Value?.ToString()?.Trim();
                            var departmentName = workSheet?.Cells[row, 7]?.Value?.ToString()?.Trim();
                            var checkPositionName = CheckCoincidence(positions, positionName, "PositionName");

                            var checkDepartmentName = CheckCoincidence(departments, departmentName, "DepartmentName");

                            employeeImportDto = new EmployeeImportDto
                            {
                                EmployeeId = Guid.NewGuid(),
                                EmployeeCode = workSheet?.Cells[row, 2]?.Value?.ToString()?.Trim(),
                                EmployeeName = workSheet?.Cells[row, 3]?.Value?.ToString()?.Trim(),
                                Gender = ConvertGender(gender),
                                DOB = dob != "" && dob != null ? ProcessDate(dob) : null,
                                PositionId = checkPositionName != null ? (Guid)checkPositionName.GetType().GetProperty("PositionId")?.GetValue(checkPositionName, null) : Guid.Empty,
                                PositionName = positionName,
                                DepartmentId = checkDepartmentName != null ? (Guid)checkDepartmentName.GetType().GetProperty("DepartmentId")?.GetValue(checkDepartmentName, null) : Guid.Empty,
                                DepartmentName = departmentName,
                                BankAccount = workSheet?.Cells[row, 8]?.Value?.ToString()?.Trim(),
                                BankName = workSheet?.Cells[row, 9]?.Value?.ToString()?.Trim(),
                            };
                            bool check = true;
                            if (checkDepartmentName == null)
                            {
                                AddImportError(employeeImportDto, $"{MISA.AMISDemo.Core.Resource.Resource_VN.FindDepartment}: {departmentName}");
                                check = false;
                            }
                            if (checkPositionName == null)
                            {
                                AddImportError(employeeImportDto, $"{MISA.AMISDemo.Core.Resource.Resource_VN.FindPosition}: {positionName}");
                                check = false;
                            }

                            var employee = _mapper.Map<Employee>(employeeImportDto);

                            // không check thừa tài khoản của người dùng 
                            var checkEmployeeCode = await _employeeRepository.CheckEmployeeCode(employeeImportDto.EmployeeCode);
                            if (checkEmployeeCode != null)
                            {
                                AddImportError(employeeImportDto, MISA.AMISDemo.Core.Resource.Resource_VN.EmployeeCode);
                                check = false;
                            }
                            if (check == true)
                            {
                                countSuccess++;
                                employeeImportDto.IsImported = true;
                                employeeImportSuccess.Add(employee);
                            }
                            if (checkEmployeeCode != null || checkDepartmentName == null || checkPositionName == null)
                            {
                                countFail++;
                            }

                            employeeImportDtos.Add(employeeImportDto);

                        }
                    }
                    employeeImportParentDtos.CountSuccess = countSuccess;
                    employeeImportParentDtos.CountFail = countFail;
                    employeeImportParentDtos.EmployeeImportDtos = employeeImportDtos;

                    var cacheKey = $"excel-import-data-{Guid.NewGuid()}"; // Use a unique key
                    employeeImportParentDtos.IdImport = cacheKey;
                    DateTimeOffset expiryTime = DateTimeOffset.Now.AddDays(1);
                    _cacheService.SetData<string>(cacheKey, JsonConvert.SerializeObject(employeeImportSuccess), expiryTime);
                }
            }

            return employeeImportParentDtos;
        }

        public int ImportDatabase(string idImport)
        {
            if (idImport == null)
            {
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.IdImport);
            }
            var dataImport = _cacheService.GetData<string>(idImport);
            var jArray = JsonConvert.DeserializeObject<JArray>(dataImport);
            var employees = jArray?.ToObject<List<Employee>>();

            var create = _employeeRepository.InsertMany(employees);
            return create;
        }
    }
}
