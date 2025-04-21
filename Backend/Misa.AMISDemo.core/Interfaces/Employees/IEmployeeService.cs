using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.DTOs.Employees;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Employees
{
    /// <summary>
    /// Chứa các nghiệp vụ của employee 
    /// </summary>
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {

        /// <summary>
        /// Tên hàm Filter và join và paging theo Employee  
        /// </summary>
        /// <param name="pageSize">Kích cỡ của trang</param>
        /// <param name="pageNumber">Tên trang </param>
        /// <param name="search">Chuỗi tìm kiếm </param>
        /// <returns></returns>
        public Task<EmployeeCountDto> FindAllFilter(int pageSize = 10, int pageNumber = 1,string search = "", string? email = "");

        /// <summary>
        /// Tên hàm: export excel bảng nhân viên  
        /// </summary>
        /// <param name="data">Dữ liệu muốn export </param>
        /// <returns></returns>
        public Task<byte[]> ExportExcel(int checkData = 1, List<Guid>? Ids = null);
        /// <summary>
        /// Tên hàm : generate mã code cho thực thể 
        /// </summary>
        /// <returns>Mã code được generate</returns>
        public Task<string> GenerateCode();

        /// <summary>
        /// Tên hàm: Import nhân viên trong  file excel 
        /// </summary>
        /// <param name="formfile">truyền vào một file excel</param>
        /// <returns>true: danh sách các lỗi hoặc thành công của mỗi bản ghi khi thêm </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
     
        public Task<EmployeeImportParentDto> ImportExcel(IFormFile formFile);

        /// <summary>
        /// Tên hàm: Import nhân viên trong và database
        /// </summary>
        /// <param name="idCache">phần tử valid được lưu trong cache </param>
        /// <returns>true: số bản ghi được create  </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 

        public int ImportDatabase(string idCache);


       

    }
}
