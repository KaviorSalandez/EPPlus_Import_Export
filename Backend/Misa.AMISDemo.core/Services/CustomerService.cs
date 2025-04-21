using AutoMapper;
using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.Const;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Core.Services.Base;
using MISA.AMISDemo.Core.UnitOfWorks;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMISDemo.Core.Services
{
    /// <summary>
    /// service riêng của customer 
    /// </summary>
    public class CustomerService : BaseService<Customer, CustomerDto, CustomerCreateDto, CustomerUpdateDto>, ICustomerService
    {
        ICustomerRepository _customerRepository;
        IMapper _mapper;
        IUnitOfWork _uow;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IUnitOfWork uow) : base(customerRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _uow = uow;
        }


       
        public override Customer MapCreateDtoToEntity(CustomerCreateDto entity)
        {
            var entityDto = _mapper.Map<Customer>(entity);
            entityDto.CustomerId = Guid.NewGuid();
            return entityDto;
        }

        public override CustomerDto MapEntityToDto(Customer entity)
        {
            var entityDto = _mapper.Map<CustomerDto>(entity);
            return entityDto;
        }


        public override Customer MapUpdateDtoToEntity(CustomerUpdateDto updateDto, Customer entity)
        {
            var entityDto = _mapper.Map(updateDto, entity);
            return entityDto;
        }

        private void CheckFileImport(IFormFile fileImport)
        {
            if (fileImport == null || fileImport.Length < 0)
            {
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.EmptyFile);
            }
            if (!Path.GetExtension(fileImport.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Không hợp lêj ");
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.FormatFileExcel);
            }
        }
        private DateTime ProcessDate(string date)
        {
            DateTime resultDateTime;

            // Mảng các định dạng ngày có thể xuất hiện trong chuỗi
            string[] dateFormats = { "yyyy-MM-dd", "MM/dd/yyyy", "dd/MM/yyyy", "yyyy/MM/dd" };

            // Thực hiện chuyển đổi
            if (DateTime.TryParseExact(date, dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out resultDateTime))
            {
                // Chuyển đổi thành công, trả về giá trị DateTime
                return resultDateTime;
            }
            else
            {
                // Xử lý khi chỉ nhập năm
                if (int.TryParse(date, out int year))
                {
                    return new DateTime(year, 1, 1);
                }

                // Xử lý khi nhập tháng/năm
                if (DateTime.TryParseExact($"01/{date}", dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out resultDateTime))
                {
                    return resultDateTime;
                }

                // Trường hợp không thể chuyển đổi, trả về DateTime.MinValue
                return DateTime.MinValue;
            }
        }


        public async Task<CustomerImportParentDto> ImportExcel(IFormFile fileImport)
        {
            CheckFileImport(fileImport);
            int countSuccess = 0, countFail = 0;
            var customerImportDtos = new List<CustomerImportDto>();
            var customerImportParentDtos = new CustomerImportParentDto();

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
                        for (int row = 2; row <= rowCount; row++)
                        {
                            var dob = workSheet.Cells
                                [row, 5]?.Value?.ToString()?.Trim();

                            var customerImportDto = new CustomerImportDto
                            {
                                CustomerId = Guid.NewGuid(),
                                CustomerCode = workSheet?.Cells[row, 2]?.Value?.ToString()?.Trim(),
                                Fullname = workSheet?.Cells[row, 3]?.Value?.ToString()?.Trim(),
                                Email = workSheet?.Cells[row, 4]?.Value?.ToString()?.Trim(),
                                DateOfBirth = ProcessDate(dob),
                                PhoneNumber = workSheet?.Cells[row, 8]?.Value?.ToString()?.Trim()
                            };
                            Console.WriteLine("Code là ");
                            Console.WriteLine(customerImportDto.CustomerCode);

                            var customer = _mapper.Map<Customer>(customerImportDto);
                            // kiểm tra xem import được không 
                            // kiểm tra trùng mã 

                            var checkCustmomer = _customerRepository.CheckCustomerCode(customer.CustomerCode);
                            if (checkCustmomer != null)
                            {
                                customerImportDto.Errors.Add("Mã " + customer.CustomerCode + " Bị trùng rồi nhé ");
                                customerImportDto.IsImported = false;
                                countFail++;
                            }
                            else
                            {
                                // ko trùng 

                                var res = await _customerRepository.CreateOne(customer);
                                if (res > 0)
                                {
                                    countSuccess++;
                                    // add thành công 
                                    customerImportDto.IsImported = true;
                                }
                                else
                                {
                                    countFail++;
                                    customerImportDto.IsImported = false;
                                }
                            }
                            // return về: count, customer 


                            customerImportDtos.Add(customerImportDto);

                        }

                        _uow.Commit();
                    }
               customerImportParentDtos.CountSuccess = countSuccess;
               customerImportParentDtos.CountFail = countFail;
                    customerImportParentDtos.CustomerImportDtos = customerImportDtos;

                }
            }

            return customerImportParentDtos;
        }
    }
}
