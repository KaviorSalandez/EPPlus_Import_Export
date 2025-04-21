using Dapper;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Departments;
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
    // ẻm iDepartment kia thì được hành động riêng. và tý nữa tiêm em nó 
    // 
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        IMISAdbContext _context;
        IUnitOfWork _uow;
        public DepartmentRepository(IMISAdbContext context, IUnitOfWork uow) : base(context)
        {
            _context = context;
            _uow = uow;
        }

        public async Task<Department> CheckDepartmentName(string DepartmentName)
        {
                string sql = "Select * from Department where DepartmentName = @departmentName";
                DynamicParameters param = new DynamicParameters();
                param.Add("departmentName", DepartmentName.Trim());
                var findName = await _uow.Connection.QueryFirstOrDefaultAsync<Department>(sql, param, transaction: _uow.Transaction);
                return findName;
        }
    }
}
