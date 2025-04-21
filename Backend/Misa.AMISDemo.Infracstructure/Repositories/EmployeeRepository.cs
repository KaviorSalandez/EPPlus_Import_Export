using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Employees;
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
    // ẻm iemployee kia thì được hành động riêng. và tý nữa tiêm em nó 
    // 
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        IMISAdbContext _context;
        IUnitOfWork _uow;

        public EmployeeRepository(IMISAdbContext context, IUnitOfWork uow) : base(context)
        {
            _context = context;
            _uow = uow;
        }

        public  async Task<Employee> CheckEmployeeCode(string employeeCode)
        {
            var sql = $"SELECT * FROM Employee WHERE EmployeeCode = @employeeCode";
            var param = new DynamicParameters();
            param.Add("employeeCode", employeeCode.ToLower().Trim());
            var entity =await  _uow.Connection.QueryFirstOrDefaultAsync<Employee>(sql, param, transaction: _uow.Transaction);

            return entity;
        }  
        public async Task<Employee> CheckBankAccount(string bankAccount)
        {
            var sql = $"SELECT * FROM Employee WHERE BankAccount  = @bankAccount";
            var param = new DynamicParameters();
            param.Add("bankAccount", bankAccount.Trim());
            var entity = await _uow.Connection.QueryFirstOrDefaultAsync<Employee>(sql, param, transaction: _uow.Transaction);

            return entity;
        }
        public async Task<IEnumerable<Employee>> FindManyRecord(List<Guid> Ids)
        {
            // Tạo danh sách tham số và chuỗi các tham số
            var parameters = new DynamicParameters();
            var paramList = new List<string>();

            // Tạo tham số cho mỗi ID trong danh sách
            for (int i = 0; i < Ids.Count; i++)
            {
                var paramName = $"@Id{i}";
                parameters.Add(paramName, Ids[i]);
                paramList.Add(paramName);
            }
            var sql = $"select * FROM `Employee` e JOIN `Department` d ON e.`DepartmentId` = d.`DepartmentId` JOIN `Position` p ON e.`PositionId` = p.`PositionId` WHERE EmployeeId IN ({string.Join(", ", paramList)})";

            var employees = await _uow.Connection.QueryAsync<Employee>(sql, parameters, transaction: _uow.Transaction);

            return employees;
        }

    }
}
