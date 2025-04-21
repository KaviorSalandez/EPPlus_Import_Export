using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.CustomerGroups
{
    // interface chứa xử lý của nhóm khách hàng 
    // created by: khanhddq
    // created at: 1/20/2023
    public interface ICustomerGroupRepository : IBaseRepository<CustomerGroup>
    {
        /// <summary>
        /// Tên hàm: tìm bản ghi theo tên nhóm khách hàng 
        /// </summary>
        /// <param name="customerGroupName">tên nhóm khách hàng  muốn tìm  </param>
        /// <returns>true: trả về nhóm khách hàng 
        /// false: trả về null </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        public CustomerGroup CheckCustomerGroupName(String customerGroupName);
    }
}
