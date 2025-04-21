using AutoMapper;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Mapping
{
    public class CustomerProfile : Profile 
    {
        public CustomerProfile() {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();
            CreateMap<Customer, CustomerImportDto>().ReverseMap();

        }
    }
}
