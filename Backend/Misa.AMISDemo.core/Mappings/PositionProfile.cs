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
    public class PositionProfile : Profile 
    {
        public PositionProfile() {
            CreateMap<Position, PositionDto>();
            CreateMap<PositionCreateDto, Position>();
            CreateMap<PositionUpdateDto, Position>();
        }
    }
}
