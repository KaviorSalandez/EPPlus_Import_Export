using AutoMapper;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.DTOs.Customers;

using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Departments;
using MISA.AMISDemo.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services
{
    /// <summary>
    /// service riêng của Department
    /// </summary>
    public class DepartmentService : BaseReadOnlyService<Department, DepartmentDto>, IDepartmentService
    {
        IDepartmentRepository _DepartmentRepository;
        IMapper _mapper;
        public DepartmentService(IDepartmentRepository DepartmentRepository, IMapper mapper) : base(DepartmentRepository)
        {
            _DepartmentRepository = DepartmentRepository;
            _mapper = mapper;
        }

        public override DepartmentDto MapEntityToDto(Department entity)
        {
            var entityDto = _mapper.Map<DepartmentDto>(entity);
            return entityDto;
        }


      
    }
}
