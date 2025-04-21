using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.CustomerGroups
{
    // interface chứa các nghiệp vụ của khách hàng 
    // created by: khanhddq
    // created at: 1/20/2023
    public interface ICustomerGroupService: IBaseReadonlyService<CustomerGroupDto>
    {
    }
}
