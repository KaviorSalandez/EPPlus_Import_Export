using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Base
{
    // repo chính 
    // created by: khanhddq
    // created at: 1/20/2023
    // khi t là kiểu class
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Tên hàm: Lấy tất cả các bản ghi theo T 
        /// </summary>
        /// <param name="filter">Object muốn lọc </param>
        /// <returns>Danh sách các bản ghi của T </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<IEnumerable<TEntity>> FindAll();
        /// <summary>
        /// Tên hàm: tìm bản ghi kiểu T theo Id
        /// </summary>
        /// <param name="Id">Id của T muốn tìm </param>
        /// <returns>true: trả về T
        /// false: trả về null </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<TEntity> FindOne(Guid Id);

        /// <summary>
        /// Tạo bản ghi 
        /// </summary>
        /// <param name="entity">Truyền vào thực thể muốn tạo </param>
        /// <returns>số bản ghi được tạo </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> CreateOne(TEntity entity);
        /// <summary>
        /// Tên hàm: Cập nhật bản ghi 
        /// </summary>
        /// <param name="entity">Thực thể muốn cập nhật </param>
        /// <param name="Id">Id của thực thể muốn cập nhật </param>
        /// <returns> số bản ghi được cập nhật </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> UpdateOne(TEntity entity, Guid Id);
        /// <summary>
        ///Tên hàm: Xóa bản ghi 
        /// </summary>
        /// <param name="Id">Id của thực thể muốn xóa </param>
        /// <returns>số bản ghi được xóa </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> DeleteOne(Guid Id);
        /// <summary>
        ///Tên hàm: Xóa nhiều bản ghi 
        /// </summary>
        /// <param name="Id">Mảng Id của thực thể muốn xóa </param>
        /// <returns>số bản ghi được xóa </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> DeleteMany(List<Guid> Ids);
        /// <summary>
        /// Tên hàm Filter và join và paging theo TEntity 
        /// </summary>
        /// <param name="pageSize">Kích cỡ của trang</param>
        /// <param name="pageNumber">Tên trang </param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> FindAllFilter(int pageSize = 10, int pageNumber = 1, string search = "", string? email = ""); 
        
        /// <summary>
        /// Tên hàm: tìm object theo TEntity 
        /// </summary>
        /// <returns></returns>
        public Task<TEntity> FindOneJoin(Guid Id);

        /// <summary>
        /// Tên hàm : generate mã code cho thực thể 
        /// </summary>
        /// <returns>Mã code được generate</returns>
        public Task<string> GenerateCode<T>();
        /// <summary>
        ///Tên hàm: thêm nhiều bản ghi 
        /// </summary>
        /// <param name="Id">Id của thực thể muốn xóa </param>
        /// <returns>số bản ghi được xóa </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        public int InsertMany(List<TEntity> Ids);
    }
}
