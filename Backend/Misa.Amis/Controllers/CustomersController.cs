using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Core.Interfaces.Customers;
using System;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    // controller của khách hàng 
    // createdby: Khanhddq
    // Created at: 12/30/2023
    public class CustomersController : BaseReadOnlyController<CustomerDto>
    {
        ICustomerService _customerService;
        public CustomersController(ICustomerService customerService) : base(customerService)
        {
           _customerService = customerService;

        }
        [HttpPost]
        [Route("ImportExcel")]
        /// <summary>
        /// Tên hàm: import khách hàng 
        /// </summary>
        /// <param name="fileImport">file Import</param>
        /// <param name="checkStep">Kiểm tra bước import</param>
        /// <returns>trả về các khách hàng được import  </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/1/20 

        public IActionResult ImportCustomer([FromForm] IFormFile fileImport)
        {
            Console.WriteLine("hello ");
            var imports = _customerService.ImportExcel(fileImport);
            return StatusCode(200,imports);
        } 
        

    }

}
