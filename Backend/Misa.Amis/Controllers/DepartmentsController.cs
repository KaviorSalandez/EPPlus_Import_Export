using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Caches;
using MISA.AMISDemo.Core.Interfaces.Departments;
using MISA.AMISDemo.Core.Interfaces.Departments;
using System;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    // controller của văn phòng
    // createdby: Khanhddq
    // Created at: 12/30/2023
    public class DepartmentsController : BaseReadOnlyController<DepartmentDto>
    {
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
        }

    }
}
