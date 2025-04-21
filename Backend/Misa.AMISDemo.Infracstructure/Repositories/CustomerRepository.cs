using Dapper;
using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Core.UnitOfWorks;
using MISA.AMISDemo.Infracstructure.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMISDemo.Infracstructure.Repository
{
    // kế thừa từ basrepo: chứa thao tác cụ thể với csdl
    // ICustomerrepo: nhằm chứa các method riêng của customer (và tý dùng để tiêm) 
    // không cần viết các method vì baseRepo cũng đã kế thừa từ ibase và nó cũng tự xử lý rồi 
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {

        IMISAdbContext _context;
        IUnitOfWork _uow;
        public CustomerRepository(IMISAdbContext context, IUnitOfWork uow) : base(context)
        {
            _context = context;
            _uow = uow;
        }

        
        public Customer CheckCustomerCode(string customerCode)
        {
            var sql = $"SELECT * FROM Customer WHERE CustomerCode = @customerCode ";
            var param = new DynamicParameters();
            param.Add("customerCode", customerCode);
            var entity = _uow.Connection.QueryFirstOrDefault<Customer>(sql, param, transaction: _uow.Transaction);

            return entity;
        }

         }
}
