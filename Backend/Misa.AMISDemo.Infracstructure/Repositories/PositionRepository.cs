using Dapper;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Positions;
using MISA.AMISDemo.Core.UnitOfWorks;
using MISA.AMISDemo.Infracstructure.Interfaces;
using MISA.AMISDemo.Infracstructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infracstructure.Repositories
{
    // ẻm này kế thừa từ base và dùng các hành động của cha nó là base
    // ẻm iPosition kia thì được hành động riêng. và tý nữa tiêm em nó 
    // 
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        IMISAdbContext _context;

        IUnitOfWork _uow;
        public PositionRepository(IMISAdbContext context, IUnitOfWork uow) : base(context)
        {
            _context = context;
            _uow = uow;
        }

        public async Task<Position> CheckPositionName(string PositionName)
        {
                string sql = "Select * from Position where PositionName = @PositionName";
                DynamicParameters param = new DynamicParameters();
                param.Add("PositionName", PositionName.Trim());
                var findName = await _uow.Connection.QueryFirstOrDefaultAsync<Position>(sql, param, transaction: _uow.Transaction);
                return findName;
        }
    }
}
