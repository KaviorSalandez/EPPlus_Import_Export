using AutoMapper;
using MISA.AMISDemo.Core.DTOs.Accounts;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Mapping
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountCreateDto, Account>();
            CreateMap<AccountUpdateDto, Account>();
        }
    }
    }
