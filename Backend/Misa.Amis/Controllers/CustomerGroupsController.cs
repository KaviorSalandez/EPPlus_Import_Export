using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.CustomerGroups;
using System;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    // controller của nhóm khách hàng
    // createdby: Khanhddq
    // Created at: 12/30/2023
    public class CustomerGroupsController : BaseReadOnlyController<CustomerGroupDto>
    {
        public CustomerGroupsController(ICustomerGroupService customerGroupService) : base(customerGroupService)
        {
        }
    }
}
