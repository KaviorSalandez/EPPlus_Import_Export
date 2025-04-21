using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;

namespace MISA.AMISDemo.Core.Interfaces.Customers
{
    /// <summary>
    /// Chứa các nghiệp vụ của customer 
    /// </summary>
    /// <typeparam name="T">T là generic type được truyền tới </typeparam>
    public interface ICustomerService : IBaseService<CustomerDto,CustomerCreateDto,CustomerUpdateDto >
    {
        /// <summary>
        /// Tên hàm: Import khách hàng trong  file excel 
        /// </summary>
        /// <param name="formfile">truyền vào một file excel</param>
        /// <returns>true: danh sách các lỗi hoặc thành công của mỗi bản ghi khi thêm </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        ///  
        public Task<CustomerImportParentDto> ImportExcel(IFormFile formFile);


        
    }
}
