using Dapper;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Infracstructure.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infracstructure.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {

        IMISAdbContext _context;


        public BaseRepository(IMISAdbContext context)
        {
            _context = context;

        }
        public async Task <int> CreateOne(TEntity entity)
        {
            var num = await _context.CreateOne<TEntity>(entity);
            return num;
        }

        public async Task<int> DeleteMany(List<Guid> Ids)
        {
            var delete = await _context.DeleteMany<TEntity>(Ids);
            return delete;


        }
        public int InsertMany(List<TEntity> Ids)
        {
            var delete = _context.InsertMany<TEntity>(Ids);
            return delete;
        }

        public async Task<int> DeleteOne(Guid Id)
        {
            var delete = await _context.DeleteOne<TEntity>(Id);
            return delete;

        }
        public async Task<IEnumerable<TEntity>> FindAll()
        {
            IEnumerable<TEntity> entities;
                entities = await _context.FindAll<TEntity>();

            return entities;
        }


        public Task<IEnumerable<TEntity>> FindAllFilter(int pageSize = 10, int pageNumber = 1, string search = "", string? email = "")
        {
            var entities = _context.FindAllFilter<TEntity>(pageSize, pageNumber, search, email);
            return entities;
        }

        public async Task<TEntity> FindOne(Guid Id)
        {

            var entity = await  _context.FindOne<TEntity>(Id);

            return entity;
        }

        public async Task<TEntity> FindOneJoin(Guid Id)
        {
            var entity = await _context.FindOneJoin<TEntity>(Id);
            return entity;
        }

        public async Task<string> GenerateCode<T>()
        {
            var code = await _context.GenerateCode<TEntity>();

            return code;
        }

        public async Task<int> UpdateOne(TEntity entity, Guid Id)
        {
            var num = await _context.UpdateOne<TEntity>(entity, Id);
            return num;
        }

       
    }
}
