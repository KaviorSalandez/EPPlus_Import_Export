using AutoMapper;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.DTOs.Employees;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Mapping
{
    public class EmployeeProfile: Profile 
    {
        public EmployeeProfile() {
         CreateMap<Employee,EmployeeDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<EmployeeExcelDto, EmployeeDto>().ReverseMap();
            CreateMap<EmployeeExcelDto, Employee>().ReverseMap();

            CreateMap<Employee, EmployeeImportDto>().ReverseMap();
        }
    }
}
