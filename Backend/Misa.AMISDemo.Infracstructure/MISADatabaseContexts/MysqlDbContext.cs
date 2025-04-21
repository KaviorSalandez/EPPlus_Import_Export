using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.Extensions.Configuration;
using MISA.AMISDemo.Core;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.UnitOfWorks;
using MISA.AMISDemo.Infracstructure.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;
using Z.Dapper.Plus;



namespace MISA.AMISDemo.Infracstructure.MISADatabaseContexts
{
    public class MysqlDbContext : IMISAdbContext
    {
        //public IDbConnection connect { get; }
        public IDbTransaction Transaction { get; set; }

        protected readonly IUnitOfWork Uow;
        public string _tableName;

        public MysqlDbContext(IUnitOfWork uow)
        {
            Uow = uow;
        }


        public async Task<IEnumerable<Type>> FindAllFilter<Type>(int pageSize = 10, int pageNumber = 1, string search = "", string? email ="")
        {
            _tableName = typeof(Type).Name;
            var nameProc = "Proc_Get" + _tableName+"s";
            var param = new DynamicParameters();
            param.Add("pageSize", pageSize);
            param.Add("pageNumber", pageNumber);
            param.Add("search", search);
            param.Add("email", email);
            var eitities = await Uow.Connection.QueryAsync<Type>(nameProc,param, commandType: CommandType.StoredProcedure, transaction: Uow.Transaction);

            return eitities;
        }
        public async Task<int> CreateOne<T>(T entity)
        {
            // Determine table name directly from entity type
            var tableName = typeof(T).Name;

            // Construct stored procedure name efficiently
            var procedureName = "Proc_Insert" + tableName;

            // Populate dynamic parameters directly
            var parameters = new DynamicParameters();
            foreach (var property in entity.GetType().GetProperties())
            {
                // Skip properties belonging to the specified namespace
                    parameters.Add(property.Name, property.GetValue(entity));
            }

            // Execute stored procedure with proper transaction handling
            var create = await Uow.Connection.ExecuteAsync(
                procedureName,
                parameters,
                commandType: CommandType.StoredProcedure, transaction: Uow.Transaction);
            return create;
        }


        public async Task<int> DeleteOne<Type>(Guid Id)
        {
                _tableName = typeof(Type).Name;
                Console.WriteLine(_tableName);
                var props = typeof(Type).GetProperties();

                var namIdOfT = props[0].Name;
                // lấy ra thuộc tính của T cái mà muốn lấy làm khóa 
                var sql = $"Delete From {_tableName} where {namIdOfT} = @Id";
                var param = new DynamicParameters();
                param.Add("Id", Id);
                var delete = await Uow.Connection.ExecuteAsync(sql,param, transaction: Uow.Transaction);
                return delete;
        }

        public async Task<IEnumerable<Type>> FindAll<Type>()
        {
            _tableName = typeof(Type).Name;
            var sql = $"select * from {_tableName}";
            var customers = await Uow.Connection.QueryAsync<Type>(sql, transaction: Uow.Transaction);
            return customers;
        }
        public async Task<Type> FindOne<Type>(Guid Id)
        {
            _tableName = typeof(Type).Name;
            // no gui ve cho minh cac object ma 
            var tableName = typeof(Type).Name;
            var props = typeof(Type).GetProperties();
            var sql = $"SELECT * FROM {tableName} WHERE {tableName}Id = @Id ";
            var param = new DynamicParameters();
            param.Add("Id", Id);
            var entity = await Uow.Connection.QueryFirstOrDefaultAsync<Type>(sql, param,transaction: Uow.Transaction);

            return entity;
        }

        public async Task<int> UpdateOne<Type>(Type entity, Guid Id)
        {
            _tableName = typeof(Type).Name;
            // colnameList chuyển thành các kí tự
            Console.WriteLine(entity.ToString());
            // lấy ra các properties của đối tượng
            var props = typeof(Type).GetProperties();
            var param = new DynamicParameters();
           
            
            foreach (var prop in props)
            {
                
                    param.Add($"@{prop.Name}", prop.GetValue(entity));
                Console.WriteLine(prop.Name, prop.GetValue(entity));
            }
            var nameProc = "Proc_Update" + _tableName;
            // rồi truyền các props vào một biết tên kiểu dynamic param
            //var sql = $"UPDATE {_tableName} set {updateProps} Where {_tableName}Id = '{Id}'  ";
            var num = await Uow.Connection.ExecuteAsync(nameProc, param, commandType: CommandType.StoredProcedure, transaction: Uow.Transaction); 
            return num;
        }

        public async Task<string> GenerateCode<Type>()
        {
            _tableName = typeof(Type).Name;
            //GetNextEmployeeCode
            var nameProc = "Proc_GetNewCode";
            var num = await Uow.Connection.QueryFirstOrDefaultAsync<string>(nameProc, commandType: CommandType.StoredProcedure);
            return num;
        }

        public async Task<Type> FindOneJoin<Type>(Guid id)
        {
            _tableName = typeof(Type).Name;
            var nameProc = "Proc_Get" + _tableName+"ById";
            var param = new DynamicParameters();
            param.Add($"{_tableName}Id", id);
        
            var eitities = await Uow.Connection.QueryFirstOrDefaultAsync<Type>(nameProc, param, commandType: CommandType.StoredProcedure, transaction: Uow.Transaction);

            return eitities;
        }

        public async Task<int> DeleteMany<Type>(List<Guid> Ids)
        {
            _tableName = typeof(Type).Name;

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

            // Tạo câu lệnh SQL sử dụng IN và thực hiện xóa
            var sql = $"DELETE FROM {_tableName} WHERE {_tableName}Id IN ({string.Join(", ", paramList)})";
            var delete = await Uow.Connection.ExecuteAsync(sql, parameters, transaction: Uow.Transaction);

            // Trả về số lượng bản ghi đã bị xóa
            return delete;
        }
       

        public int InsertMany<Type>(List<Type> entities)
        {
            _tableName = typeof(Type).Name;
            var context = new DapperPlusContext(Uow.Connection);
            //context.Entity<T>().KeepIdentity(keepIdentity);
            var bulkResult = context.BulkInsert(entities);
            Console.WriteLine(bulkResult.CurrentItem);
            return bulkResult.CurrentItem.Count;
        }

       
    }
}

