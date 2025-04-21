using AutoMapper;
using DocumentFormat.OpenXml.Vml.Office;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.CustomerGroups;
using MISA.AMISDemo.Core.Interfaces.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMISDemo.Core.Services.Base
{

    /// <summary>
    /// service chính của các entities 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<TEntity, TDto, TCreateDto, TUpdateDto> : BaseReadOnlyService<TEntity,TDto>, IBaseService<TDto, TCreateDto, TUpdateDto> where TEntity : IEntity
    {
        IBaseRepository<TEntity> _repository;
        public string _tableName;
        private TDto result;

        public BaseService(IBaseRepository<TEntity> repository) : base(repository)
        {
            _repository = repository;
            _tableName = typeof(TEntity).Name;

        }
        public async Task<int> DeleteService(Guid Id)
        {
            // kiểm tra id
            if (Id == Guid.Empty)
            {
                throw new ValidateException(Resource.Resource_VN.ValidateId);
            }
            var find = await _repository.FindOne(Id);
            if (find == null)
            {
                throw new ValidateException(Resource.Resource_VN.FindObject);
            }
            var deleteOne = await _repository.DeleteOne(Id);
            return deleteOne;
        }

        public async Task<int> InsertService(TCreateDto TCreateDTO)
        {
            await CheckBeforeInsert(TCreateDTO);
            var entity = MapCreateDtoToEntity(TCreateDTO);
            await ValidateObject(entity);
            // kiểm tra entity 
            if (entity is IEntity baseEntity)
            {
                entity.ModifiedBy ??= "Khanhddq";
                entity.ModifiedDate ??= DateTime.Now;
                entity.CreatedDate ??= DateTime.Now;
                entity.CreatedBy ??= "Khanhddq";

            }
            if (_repository == null)
            {
                // Xử lý tương ứng khi _repository là null
                throw new InvalidOperationException(Resource.Resource_VN.CheckRepository);
            }

            var create = await _repository.CreateOne(entity);
            return create;
        }
        /// <summary>
        /// Kiêm tra trước khi insert dữ liệu vào 
        /// </summary>
        /// <param name="createDto">Thực thể được tạo </param>
        public virtual Task CheckBeforeInsert(TCreateDto createDto)
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// Kiểm tra trước khi cập nhật 
        /// </summary>
        /// <param name="updateDto">thực thể được cập nhật </param>
        public virtual Task CheckBeforeUpdate(TUpdateDto updateDto)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// hàm này sẽ check trùng code của các T
        /// </summary>
        /// <param name="Service">tham số truyền vào là một object của T </param>
        public virtual Task ValidateObject(TEntity Service)
        {
            return Task.CompletedTask;
        }

        public async Task<int> UpdateService(TUpdateDto updateDto, Guid Id)
        {
            await CheckBeforeUpdate(updateDto);
            string id = _tableName + "Id";

            var findOne = await _repository.FindOne(Id);

            if (findOne == null)
            {
                throw new ValidateException(Resource.Resource_VN.FindObject);
            }

            var entity = MapUpdateDtoToEntity(updateDto, findOne);
            await ValidateObject(entity);
            if (entity is IEntity baseEntity)
            {
                entity.ModifiedBy ??= "Khanhddq";
                entity.ModifiedDate ??= DateTime.Now;
            }
            if (_repository == null)
            {
                // Xử lý tương ứng khi _repository là null
                throw new InvalidOperationException(Resource.Resource_VN.CheckRepository);
            }
            var updateOne = await _repository.UpdateOne(entity, Id);
            return updateOne;
        }



        /// <summary>
        /// Map từ thực thể createdto sang entity kiểu T 
        /// </summary>
        /// <param name="entity">thực thể được tạo </param>
        /// <returns>Thực thể kiểu T </returns>
        public abstract TEntity MapCreateDtoToEntity(TCreateDto entity);

        public virtual async Task ValidateCreateBusiness(TEntity entity)
        {
            await Task.CompletedTask;
        }
        /// <summary>
        /// Map từ thực thể updatedto sang entity kiểu T 
        /// </summary>
        /// <param name="entity">thực thể được cập nhật  </param>
        /// <returns>Thực thể kiểu T </returns>
        public abstract TEntity MapUpdateDtoToEntity(TUpdateDto updateDto, TEntity entity);

        public virtual async Task ValidateUpdateBusiness(TEntity entity)
        {
            await Task.CompletedTask;
        }
        /// <summary>
        /// kiểm tra các ids được truyền tới xem có rỗng và có trong database không 
        /// </summary>
        /// <param name="Ids">Danh sách các id </param>
        /// <returns></returns>
        /// <exception cref="ValidateException">trả về exception ko có id hoặc không tồn tại trong Database</exception>
       public async Task ValidateManyIds(List<Guid> Ids)
        {
            for (int i = 0; i < Ids.Count; i++)
            {
                var Id = Ids[i];
                if (Id == Guid.Empty)
                {
                    throw new ValidateException(Resource.Resource_VN.ValidateId);
                }
            }

            for (int i = 0; i < Ids.Count; i++)
            {
                var Id = Ids[i];
                var find = await _repository.FindOne(Id);
                if (find == null)
                {
                    throw new ValidateException(Resource.Resource_VN.FindObject);
                }
            }

        }

        public async Task<int> DeleteMany(List<Guid> Ids)
        {

           await ValidateManyIds(Ids);
            var deleteOne = await _repository.DeleteMany(Ids);
            return deleteOne;
        }
    }
}
