using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services.Base
{
    public abstract class BaseReadOnlyService<TEntity, TDto> : IBaseReadonlyService<TDto>
    {
        IBaseRepository<TEntity> _repository;

        protected BaseReadOnlyService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TDto>> FindAll()
        {
            var tEntity = await _repository.FindAll();
            var result = tEntity.Select(e => MapEntityToDto(e)).ToList();
            return result;
        }
        public abstract TDto MapEntityToDto(TEntity entity);

        public async Task<TDto> FindOne(Guid id)
        {

            if (id == Guid.Empty)
            {
                throw new ValidateException(Resource.Resource_VN.ValidateId);
            }
            var nameEntity = typeof(TEntity).Name;
            var tEntity = await _repository.FindOne(id);
            if (tEntity == null)
            {
                throw new ValidateException(Resource.Resource_VN.FindObject);
            }
            var result = MapEntityToDto(tEntity);
            return result;
        }

        public async Task<TDto> FindOneJoin(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                throw new ValidateException(Resource.Resource_VN.ValidateId);
            }
            var nameEntity = typeof(TEntity).Name;
            var tEntity = await _repository.FindOneJoin(Id);
            if (tEntity == null)
            {
                throw new ValidateException(Resource.Resource_VN.FindObject);
            }
            var result = MapEntityToDto(tEntity);
            return result;
        }
    }
}
