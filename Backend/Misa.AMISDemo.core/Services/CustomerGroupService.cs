using AutoMapper;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.CustomerGroups;
using MISA.AMISDemo.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services
{
    /// <summary>
    /// service riêng của customerGroup
    /// </summary>
    public class CustomerGroupService : BaseReadOnlyService<CustomerGroup, CustomerGroupDto>, ICustomerGroupService
    {
        ICustomerGroupRepository _customerGroupRepository;
        IMapper _mapper;
        public CustomerGroupService(ICustomerGroupRepository customerGroupRepository, IMapper mapper) : base(customerGroupRepository)
        {
            _customerGroupRepository = customerGroupRepository;
            _mapper = mapper;
        }

        public override CustomerGroupDto MapEntityToDto(CustomerGroup  entity)
        {
            var entityDto = _mapper.Map<CustomerGroupDto>(entity);
            return entityDto;
        }
      
    }
}
