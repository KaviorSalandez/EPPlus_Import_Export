using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.DTOs.Employees;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Enum;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Caches;
using MISA.AMISDemo.Core.Interfaces.Departments;
using MISA.AMISDemo.Core.Interfaces.Employees;
using MISA.AMISDemo.Core.Interfaces.Positions;
using MISA.AMISDemo.Core.Services;
using MISA.AMISDemo.Core.UnitOfWorks;
using MISA.AMISDemo.Infracstructure.Interfaces;
using MISA.AMISDemo.Infracstructure.Repositories;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.AMISDemo.Core.Enum.MISAEnum;
using Microsoft.AspNetCore.Hosting;

namespace MISA.AMISDemo.Core.UnitTests.Service
{
    [TestFixture]
    public class EmployeeServiceTests
    {

        public IMISAdbContext MISADbcontext;
        public IUnitOfWork unitOfWork;
        public IEmployeeRepository employeeRepository;
        public IDepartmentRepository departmentRepository;
        public IPositionRepository positionRepository;
        public IMapper mapper;
        public EmployeeService employeeService;
        public ICacheService cacheService;
        public IFormFile mockFile;

        [SetUp]
        public void Setup()
        {
            MISADbcontext = Substitute.For<IMISAdbContext>();
            unitOfWork = Substitute.For<IUnitOfWork>();
            // employeeRepository lấy từ IEmployeeRepository. vì ko muốn phụ thuộc vào 
            employeeRepository = Substitute.For<IEmployeeRepository>();
            departmentRepository = Substitute.For<IDepartmentRepository>();
            positionRepository = Substitute.For<IPositionRepository>();
            mapper = Substitute.For<IMapper>();
            cacheService = Substitute.For<ICacheService>();
            employeeService = Substitute.For<EmployeeService>(employeeRepository, mapper, departmentRepository, positionRepository, unitOfWork, employeeRepository, cacheService);
            mockFile = Substitute.For<IFormFile>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        [Test]
        /// <summary>
        /// Test case: Kiểm tra InsertService khi EmployeeAudit không null và trả về EmployeeAudit khác null.
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void InsertService_EmployeeAuditNull_ReturnEmployeeAuditNotNull()
        {
            // Arrange
            Employee employee = new Employee();
            EmployeeCreateDto employeeCreateDto = new EmployeeCreateDto();
            // Chuẩn bị createDto 
            // Giả lập hàm fake trong employeeService để chạy được unit test 
            employeeService.MapCreateDtoToEntity(employeeCreateDto).Returns(employee);

            // Act
            // Mặc định nó sẽ chạy qua được các hàm khác vì employeeService được substitute fake 
            employeeService.InsertService(employeeCreateDto);

            // Assert 
            Assert.That(employee.CreatedBy, Is.EqualTo("Khanhddq"));
            Assert.That(employee.ModifiedBy, Is.EqualTo("Khanhddq"));
            employeeService.Received(1).CheckBeforeInsert(employeeCreateDto);
            // EmployeeRepository là con của IEmployeeRepository, trong BaseService 
            employeeRepository.Received(1).CreateOne(employee);
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra InsertService khi Repository null và trả về ngoại lệ "CheckRepository".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void InsertService_RepositoryNull_ReturnReposioryNull()
        {
            EmployeeCreateDto employeeCreateDto = new EmployeeCreateDto();
            // Chuẩn bị repository null 
            employeeRepository = null;

            // Act & Assert 
            try
            {
                var create = employeeService.InsertService(employeeCreateDto);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.CheckRepository));
            }
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra CheckBeforeUpdate khi không tìm thấy Department và trả về ngoại lệ "FindDepartment".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void CheckBeforeUpdate_DepartmentNotFound_ReturnExceptionDepartment()
        {
            // Arrange
            EmployeeUpdateDto employeeUpdateDto = new EmployeeUpdateDto();

            // Act & Assert 
            try
            {
                employeeService.CheckBeforeUpdate(employeeUpdateDto);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.FindDepartment));
            }
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra CheckBeforeUpdate khi không tìm thấy Position và trả về ngoại lệ "FindPosition".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void CheckBeforeUpdate_PositionNotFound_ReturnExceptionPosition()
        {
            // Arrange
            EmployeeUpdateDto employeeUpdateDto = new EmployeeUpdateDto();
            Department department = new Department();
            departmentRepository.FindOne(employeeUpdateDto.DepartmentId).Returns(department);

            // Act & Assert 
            try
            {
                employeeService.CheckBeforeUpdate(employeeUpdateDto);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.FindPosition));
            }
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra DeleteService khi Id rỗng và trả về ngoại lệ "ValidateId".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void DeleteService_IdEmpty_ReturnIdEmpty()
        {
            // Arrange
            Guid Id = Guid.Empty;
            Employee employee = new Employee();
            employeeRepository.FindOne(Id).Returns(employee);

            // Act & Assert 
            try
            {
                var delete = employeeService.DeleteService(Id);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.ValidateId));
            }
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra DeleteService khi không tìm thấy Employee và trả về ngoại lệ "FindObject".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void DeleteService_EmployeeNotFound_ReturnEmployeeNotFound()
        {
            // Arrange
            Guid Id = Guid.NewGuid();
            Employee employee = null;
            employeeRepository.FindOne(Id).Returns(employee);

            // Act & Assert 
            try
            {
                var delete = employeeService.DeleteService(Id);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.FindObject));
            }
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra UpdateService khi EmployeeAudit không null và trả về EmployeeAudit khác null.
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void UpdateService_EmployeeAuditNull_ReturnEmployeeAuditNotNull()
        {
            // Arrange
            Employee employee = new Employee();
            Guid Id = Guid.NewGuid();
            EmployeeUpdateDto employeeUpdateDto = new EmployeeUpdateDto();
            employeeRepository.FindOne(Id).Returns(employee);
            employeeService.MapUpdateDtoToEntity(employeeUpdateDto, employee).Returns(employee);

            // Act
            employeeService.UpdateService(employeeUpdateDto, Id);

            // Assert 
            Assert.That(employee.ModifiedBy, Is.EqualTo("Khanhddq"));
            employeeService.Received(1).CheckBeforeUpdate(employeeUpdateDto);
            employeeRepository.Received(1).UpdateOne(employee, Id);
        }
        [Test]
        /// <summary>
        /// Test case: Kiểm tra UpdateService khi Repository là null và trả về ngoại lệ "CheckRepository".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void UpdateService_RepositoryNull_ReturnReposioryNull()
        {
            // Arrange
            EmployeeUpdateDto employeeUpdateDto = new EmployeeUpdateDto();
            Employee employee = new Employee();
            Guid id = Guid.NewGuid();
            employeeRepository.FindOne(id).Returns(employee);
            employeeRepository = null;

            // Act & Assert 
            try
            {
                var create = employeeService.UpdateService(employeeUpdateDto, id);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.CheckRepository));
            }
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra FindOne khi Id rỗng và trả về ngoại lệ "FindObject".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void FindOne_IdEmpty_ReturnExceptionIdEmpty()
        {
            // Arrange
            Guid Id = Guid.Empty;
            Employee employee = null;
            employeeRepository.FindOne(Id).Returns(employee);

            // Act & Assert 
            try
            {
                var find = employeeService.FindOne(Id);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.ValidateId));
            }
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra FindOne khi không tìm thấy Employee và trả về ngoại lệ "FindObject".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void FindOne_IdNotFound_ReturnExceptionIdNotFound()
        {
            // Arrange
            Guid Id = Guid.NewGuid();
            Employee employee = null;
            employeeRepository.FindOne(Id).Returns(employee);

            // Act & Assert 
            try
            {
                var find = employeeService.FindOne(Id);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.FindObject));
            }
            employeeRepository.Received(1).FindOne(Id);
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra CheckBeforeInsert khi không tìm thấy Department và trả về ngoại lệ "FindDepartment".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void CheckBeforeInsert_DepartmentNotFound_ReturnExceptionDepartment()
        {
            // Arrange
            EmployeeCreateDto employeeCreateDto = new EmployeeCreateDto();

            // Act & Assert 
            try
            {
                employeeService.CheckBeforeInsert(employeeCreateDto);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.FindDepartment));
            }
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra CheckBeforeInsert khi không tìm thấy Position và trả về ngoại lệ "FindPosition".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void CheckBeforeInsert_PositionNotFound_ReturnExceptionPosition()
        {
            // Arrange
            EmployeeCreateDto employeeCreateDto = new EmployeeCreateDto();
            Department department = new Department();
            departmentRepository.FindOne(employeeCreateDto.DepartmentId).Returns(department);

            // Act & Assert 
            try
            {
                employeeService.CheckBeforeInsert(employeeCreateDto);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.FindPosition));
            }
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra CheckBeforeInsert khi tìm thấy EmployeeCode và trả về ngoại lệ "EmployeeCode".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void CheckBeforeInsert_EmployeeCodeFound_ReturnExceptionEmployeeCode()
        {
            // Arrange
            EmployeeCreateDto employeeCreateDto = new EmployeeCreateDto();
            Department department = new Department();
            Position position = new Position();
            departmentRepository.FindOne(employeeCreateDto.DepartmentId).Returns(department);
            positionRepository.FindOne(employeeCreateDto.PositionId).Returns(position);

            // Act & Assert 
            try
            {
                employeeService.CheckBeforeInsert(employeeCreateDto);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.EmployeeCode));
            }
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra DeleteMany khi một trong các Id rỗng và trả về ngoại lệ "ValidateId".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void DeleteMany_IdsEmpty_ReturnExceptionIdEmpty()
        {
            // Arrange 
            List<Guid> ids = new List<Guid>() { Guid.NewGuid(), Guid.Empty, Guid.NewGuid() };

            // Act & Assert 
            try
            {
                employeeService.DeleteMany(ids);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.ValidateId));
            }
        }

        [Test]
        /// <summary>
        /// Test case: Kiểm tra DeleteMany khi không tìm thấy Employee và trả về ngoại lệ "FindObject".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void DeleteMany_IdNotFound_ReturnExceptionIdNotFound()
        {
            // Arrange 
            List<Guid> ids = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            // Act & Assert 
            try
            {
                employeeService.DeleteMany(ids);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.FindObject));
            }
        }


        [TestCase("nam", MISAEnum.Gender.Nam)]
        [TestCase("nữ", MISAEnum.Gender.Nữ)]
        [TestCase("khác", MISAEnum.Gender.Khác)]
        /// <summary>
        /// Kiểm tra phương thức ConvertGender khi đầu vào là hợp lệ và trả về giới tính chính xác.
        /// </summary>
        /// <param name="gender">Giới tính đầu vào.</param>
        /// <param name="expectedResult">Kết quả giới tính mong đợi.</param>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void ConvertGender_ValidInput_ReturnsCorrectGender(string gender, Gender expectedResult)
        {
            // arrange

            // act
            var result = employeeService.ConvertGender(gender);
            // assert 
            Assert.That(result, Is.EqualTo(expectedResult));
        }


        [TestCase("1990", "01/01/1990")]
        [TestCase("", null)]
        [TestCase("20/01/2024", "20/01/2024")]
        [TestCase("12/1990", "01/12/1990")]
        [TestCase("1/1/1990", "01/01/1990")]
        /// <summary>
        /// Kiểm tra phương thức ProcessDate khi đầu vào là hợp lệ và trả về ngày chính xác.
        /// </summary>
        /// <param name="input">Ngày đầu vào.</param>
        /// <param name="expectedDate">Ngày mong đợi.</param>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void ProcessDate_ValidInput_ReturnsCorrectDate(string input, string expectedDate)
        {
            // Arrange

            // Act
            var result = employeeService.ProcessDate(input);

            // Assert
            if (expectedDate == null)
            {
                Assert.IsNull(result);
            }
            else
            {
                Assert.AreEqual(DateTime.ParseExact(expectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), result);
            }
        }

        [Test]
        /// <summary>
        /// Kiểm tra phương thức CheckFileImport khi đầu vào là null và trả về ngoại lệ "EmptyFile".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void CheckFileImport_NullFile_ThrowsValidateException()
        {
            // Arrange

            IFormFile nullFile = null;

            // Act & Assert
            try
            {
                employeeService.CheckFileImport(nullFile);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.EmptyFile));
            }
        }

        [Test]
        /// <summary>
        /// Kiểm tra phương thức CheckFileImport khi đầu vào là file rỗng và trả về ngoại lệ "EmptyFile".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void CheckFileImport_EmptyFile_ThrowsValidateException()
        {
            // Arrange

            var emptyFile = Substitute.For<IFormFile>();
            emptyFile.Length.Returns(0);

            // Act & Assert
            try
            {
                employeeService.CheckFileImport(emptyFile);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.EmptyFile));
            }
        }

        [Test]
        /// <summary>
        /// Kiểm tra phương thức CheckFileImport khi đầu vào là file có phần mở rộng không hợp lệ và trả về ngoại lệ "FormatFileExcel".
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void CheckFileImport_InvalidExtension_ThrowsValidateException()
        {
            // Arrange

            var invalidFile = Substitute.For<IFormFile>();
            invalidFile.Length.Returns(1);
            invalidFile.FileName.Returns("test.txt");

            // Act & Assert
            try
            {
                employeeService.CheckFileImport(invalidFile);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.FormatFileExcel));
            }
        }

        [Test]
        /// <summary>
        /// Kiểm tra phương thức CheckFileImport khi đầu vào là file hợp lệ và không trả về ngoại lệ.
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        public void CheckFileImport_ValidFile_NoExceptionThrown()
        {
            // Arrange
            var validFile = Substitute.For<IFormFile>();
            validFile.Length.Returns(1);
            validFile.FileName.Returns("test.xlsx");

            // Act & Assert
            Assert.DoesNotThrow(() => employeeService.CheckFileImport(validFile));
        }


        /// <summary>
        /// Kiểm tra id import có hợp lệ không 
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>

        [Test]
        public void ImportDatabase_IdImportInValid_ReturnExceptionIdImport()
        {
            // arrange
            string idImport = null;
            // act & assert 
            try
            {
                employeeService.ImportDatabase(idImport);

            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.IdImport));
            }
        }

        /// <summary>
        /// Kiểm tra tham số null
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        [Test]
        public void CheckCoincidence_ItemsNull_ReturnNull()
        {
            // arrange
            IEnumerable<object> items = null;
            string name = "Nhân Viên", nameCompare = "PositionId";

            // act 
            var result = employeeService.CheckCoincidence(items, name, nameCompare);

            // assert

            Assert.That(result, Is.Null);
        }
        
        /// <summary>
        /// Kiểm tra tham số null
        /// </summary>
        /// <remarks>
        ///     created by: Đặng Đình Quốc Khánh
        ///     created_at: 5/3/2024
        /// </remarks>
        [Test]
        public void CheckCoincidence_ParamValid_ReturnObject()
        {
            // arrange
            IEnumerable<object> items = new List<Object>
            {
                new {Id = 1, PositionId = "Chủ tịch"},
                new {Id = 2, PositionId = "Nhân Viên"},
                new {Id = 3, PositionId = "Giám Đốc"}
            };
            string name = "Nhân Viên", nameCompare = "PositionId";

            // act 
            var result = employeeService.CheckCoincidence(items, name, nameCompare);

            // assert

            Assert.That(result, Is.Not.Null);
        }


     
    }

}
