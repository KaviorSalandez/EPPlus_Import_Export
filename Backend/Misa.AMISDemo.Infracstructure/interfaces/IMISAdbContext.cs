using MISA.AMISDemo.Core.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMISDemo.Infracstructure.Interfaces
{
    public interface IMISAdbContext
    {
        // tạo interface; chỗ cắm điện chung 

        // connect nhằm thực hiện với db nào 
        //public IDbConnection connect { get;  } 
        //public IDbTransaction Transaction { get; set; }
        /// <summary>
        /// Tên hàm: Lấy tất cả các bản ghi theo Type 
        /// </summary>
        /// <param name="filter">Object muốn lọc </param>
        /// <returns>Danh sách các bản ghi của T </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<IEnumerable<Type>> FindAll<Type>();
        /// <summary>
        /// Tên hàm: tìm bản ghi kiểu Type theo Id
        /// </summary>
        /// <param name="Id">Id của T muốn tìm </param>
        /// <returns>true: trả về T
        /// false: trả về null </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<Type> FindOne<Type>(Guid Id);

        /// <summary>
        /// Tạo bản ghi 
        /// </summary>
        /// <param name="entity">Truyền vào thực thể muốn tạo </param>
        /// <returns>số bản ghi được tạo </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> CreateOne<Type>(Type entity);
        /// <summary>
        /// Tên hàm: Cập nhật bản ghi 
        /// </summary>
        /// <param name="entity">Thực thể muốn cập nhật </param>
        /// <param name="Id">Id của thực thể muốn cập nhật </param>
        /// <returns> số bản ghi được cập nhật </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> UpdateOne<Type>(Type entity, Guid Id);
        /// <summary>
        ///Tên hàm: Xóa bản ghi 
        /// </summary>
        /// <param name="Id">Id của thực thể muốn xóa </param>
        /// <returns>số bản ghi được xóa </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> DeleteOne<Type>(Guid Id);
        /// <summary>
        ///Tên hàm: Xóa nhiều bản ghi 
        /// </summary>
        /// <param name="Id">Id của thực thể muốn xóa </param>
        /// <returns>số bản ghi được xóa </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> DeleteMany<Type>(List<Guid> Ids );
      

        /// <summary>
        /// Tên hàm Filter và join và paging theo TEntity 
        /// </summary>
        /// <param name="pageSize">Kích cỡ của trang</param>
        /// <param name="pageNumber">Tên trang </param>
        /// <returns>danh sách các bản ghi theo T </returns>
        public Task<IEnumerable<Type>> FindAllFilter<Type>(int pageSize = 10, int pageNumber = 1, string search = "", string? email = "");    /// <summary>
        /// Tên hàm: tìm bản ghi theo Type 
        /// </summary>
        /// <param name="pageSize">Kích cỡ của trang</param>
        /// <param name="pageNumber">Tên trang </param>
        /// <returns>danh sách các bản ghi theo T </returns>
        public Task<Type> FindOneJoin<Type>(Guid id);
        /// <summary>
        /// Tên hàm : generate mã code cho thực thể 
        /// </summary>
        /// <returns>Mã code được generate</returns>
        public Task<string> GenerateCode<Type>();


        /// <summary>
        ///Tên hàm: thêm nhiều bản ghi 
        /// </summary>
        /// <param name="Id">Id của thực thể muốn xóa </param>
        /// <returns>số bản ghi được xóa </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        public int InsertMany<Type>(List<Type> Ids);
    }
}
