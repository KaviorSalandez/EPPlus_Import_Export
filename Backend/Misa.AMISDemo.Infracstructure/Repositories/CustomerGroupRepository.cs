using Dapper;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.CustomerGroups;
using MISA.AMISDemo.Core.UnitOfWorks;
using MISA.AMISDemo.Infracstructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMISDemo.Infracstructure.Repository
{
    public class CustomerGroupRepository : BaseRepository<CustomerGroup>, ICustomerGroupRepository
    {
        IMISAdbContext _context;
        IUnitOfWork _uow;
        public CustomerGroupRepository(IMISAdbContext context, IUnitOfWork uow) : base(context)
        {
            _context = context;
            _uow = uow;
        }

        public CustomerGroup CheckCustomerGroupName(string customerGroupName)
        {
            string sql = "Select CustomerGroupName from CustomerGroup where CustomerGroupName = @customerGroupName";
            DynamicParameters param = new DynamicParameters();
            param.Add("customerGroupName", customerGroupName);
            var findName = _uow.Connection.QueryFirstOrDefault<CustomerGroup>(sql, param);
            return findName;
        }

    }
}
