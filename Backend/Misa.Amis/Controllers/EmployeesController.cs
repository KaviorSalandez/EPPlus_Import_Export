using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Core.Interfaces.Employees;
using MISA.AMISDemo.Core.Interfaces.Employees;
using MISA.AMISDemo.Core.Services;
using System;
using System.Data;
using System.Reflection;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    // controller của Nhân viên 
    // createdby: Khanhddq
    // Created at: 12/30/2023
    public class EmployeesController : BaseController<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        IEmployeeService _employeeService;
       
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        
        [HttpGet]
        [Authorize]
        [Route("Filter")]

        /// <summary>
        /// Tên hàm: lấy ra tất cả các nhân viên và join các bảng liên quan 
        /// </summary>
        /// <param name="pageSize">Kích cỡ của trang</param>
        /// <param name="pageNumber">Tên trang </param>
        /// <param name="search">Chuỗi tìm kiếm </param>
        /// <returns>trả về danh sách tất cả nhân viên</returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/1/20 

        public async Task<IActionResult> FindAllFilter(int pageSize = 0, int pageNumber = 1, string? search="", string? email = "")
        {
            if(pageSize == 0)
            {
                var employees = await _employeeService.FindAll();
                pageSize = employees.Count();
            }
            if(search == null)
            {
                search = "";
            }
          
            var entities = await  _employeeService.FindAllFilter(pageSize, pageNumber,search, email);

            return StatusCode(200,entities);

        }
        [HttpGet]
        [Route("GenerateCode")]
        [ResponseCache(CacheProfileName = "Default30")]
        [Authorize]
        /// <summary>
        /// Tên hàm: tạo code mới cho nhân viên 
        /// </summary>
        /// <returns>trả về code mới cho nhân viên</returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/1/20 
        public async Task<IActionResult> GenerateCode( )
        {
            var code =  await _employeeService.GenerateCode();

            return StatusCode(200,code);

        }
        
        [HttpGet("FindJoin/{Id}")]
        [Authorize]
    
        /// <summary>
        /// Tên hàm: tìm 1 thằng nhân viên và join 
        /// </summary>
        /// <param name="Id">Id của employee muốn join</param>
        /// <returns>trả về nhân viên đó </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/1/20 
        public async Task<IActionResult> FindOneJoin(Guid Id)
        {
            var employee  = await _employeeService.FindOneJoin(Id);

            return StatusCode(200, employee);

        }  
      
        
        [HttpDelete("DeleteMany")]
        [Authorize(Roles="admin")]
        /// <summary>
        /// Tên hàm: tìm 1 xóa nhiều nhân viên
        /// </summary>
        /// <param name="Ids">Danh sách các id muốn xóa </param>
        /// <returns>trả về số bản ghi được xóa  </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/1/20 

        public async Task<IActionResult> DeleteMany([FromQuery] List<Guid> Ids)
        {
            var code = await _employeeService.DeleteMany(Ids);

            return StatusCode(200,code);

        }
        
        [HttpGet("ExportExcel")]
        [Authorize(Roles = "admin")]
        /// <summary>
        /// Tên hàm: Export excel ra các nhân viên
        /// </summary>
        /// <param name="checkData">Kiểm tra số lượng dữ liệu muốn export: 1 là export [], != 1 là export hết data  </param>
        /// <returns>file excel của nhân viên  </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/1/20 

        public async Task<IActionResult> ExportExcel([FromQuery]int checkData = 1)
        {

                byte[] excelData = await _employeeService.ExportExcel(checkData, null );
                string fileName = $"Misa_Employee_{DateTime.Now.ToString("dd/MM/yy")}.xlsx";
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpPost]
        [Route("ExportExcel")]
        [Authorize(Roles = "admin")]
        /// <summary>
        /// Tên hàm: Export excel ra các nhân viên
        /// </summary>
        /// <param name="checkData">Kiểm tra số lượng dữ liệu muốn export: 1 là export [], != 1 là export hết data  </param>
        /// <returns>file excel của nhân viên  </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/1/20 


        public async Task<IActionResult> ExportExcel2([FromBody] List<Guid>? Ids = null)

        {

            byte[] excelData = await _employeeService.ExportExcel(2, Ids);
            string fileName = $"Misa_Employee_{DateTime.Now.ToString("dd/MM/yy")}.xlsx";
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }
            [HttpPost]
        [Route("ImportValidate")]
        [Authorize(Roles = "admin")]
        /// <summary>
        /// Tên hàm: import nhân viên 
        /// </summary>
        /// <param name="fileImport">file Import</param>
        /// <param name="checkStep">Kiểm tra bước import</param>
        /// <returns>trả về các nhân viên được import  </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/1/20 
        public async Task<IActionResult> ImportEmployee([FromForm] IFormFile fileImport)
        {
            Console.WriteLine("hello ");
            var imports = await _employeeService.ImportExcel(fileImport);
            return StatusCode(200, imports);
        }
         [HttpGet("ImportExcel/{id}")]
       
        [Authorize(Roles = "admin")]
        /// <summary>
        /// Tên hàm: import nhân viên 
        /// </summary>
        /// <param name="fileImport">file Import</param>
        /// <param name="checkStep">Kiểm tra bước import</param>
        /// <returns>trả về các nhân viên được import  </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/1/20 
        public IActionResult ImportDatabase(string id)
        {
  
            var imports =  _employeeService.ImportDatabase(id);
            return StatusCode(200, imports);
        }


    }
}
