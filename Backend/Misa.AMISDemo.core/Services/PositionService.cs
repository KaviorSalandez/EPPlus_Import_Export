using AutoMapper;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Positions;
using MISA.AMISDemo.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services
{
    /// <summary>
    /// service riêng của Position
    /// </summary>
    public class PositionService : BaseReadOnlyService<Position, PositionDto>, IPositionService
    {
        IPositionRepository _PositionRepository;
        IMapper _mapper;
        public PositionService(IPositionRepository PositionRepository, IMapper mapper) : base(PositionRepository)
        {
            _PositionRepository = PositionRepository;
            _mapper = mapper;
        }

        public override PositionDto MapEntityToDto(Position entity)
        {
            var entityDto = _mapper.Map<PositionDto>(entity);
            return entityDto;
        }
    }
}
