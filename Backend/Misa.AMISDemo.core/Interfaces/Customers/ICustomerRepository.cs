using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Customers
{
    // interface chứa các method của khách hàng 
    // created by: khanhddq
    // created at: 1/20/2023
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Tên hàm: Check trùng mã khách hàng của customer
        /// </summary>
        /// <param name="customerCode">truyền vào một mã khách hàng</param>
        /// <returns>true: trả thực thể khách hàng
        /// false: trả về null </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        public Customer CheckCustomerCode(string customerCode);

       
   
    }
}
