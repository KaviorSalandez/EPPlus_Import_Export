using MISA.AMISDemo.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Base
{

    // tầng dịch vụ chính 
    // created by: khanhddq
    // created at: 1/20/2023
    public interface IBaseService<TDto, TCreateDto, TUpdateDto> : IBaseReadonlyService<TDto>
    {
        // tầng nghiệp vụ kiểm tra các id , kiểm tra customer trước khi thực hiện trong repo

        /// <summary>
        /// Tên hàm: Nghiệp vụ của insert ở mỗi kiểu T 
        /// </summary>
        /// <param name="service">Truyền vào một bản ghi kiểu T </param>
        /// <returns>số bản ghi được tạo</returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> InsertService(TCreateDto service);

        /// <summary>
        /// Tên hàm: Nghiệp vụ muốn xóa ở mỗi kiểu T 
        /// </summary>
        /// <param name="Id">Id của T muốn xóa </param>
        /// <returns>số bản ghi được xóa</returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> DeleteService(Guid Id);
        /// <summary>
        /// Tên hàm: Nghiệp vụ muốn xóa nhiều ở mỗi kiểu T 
        /// </summary>
        /// <param name="Ids">Mảng Id của T muốn xóa </param>
        /// <returns>số bản ghi được xóa</returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> DeleteMany(List<Guid> Ids);

        /// <summary>
        /// Tên hàm: Nghiệp vụ muốn xóa ở mỗi kiểu T 
        /// </summary>
        /// <param name="Id">Id của T muốn xóa </param>
        /// <returns>số bản ghi được xóa</returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        //Task<TDto> FindOneJoin(Guid Id);

        /// <summary>
        /// Tên hàm: nghiệp vụ Cập nhật thằng T 
        /// </summary>
        /// <param name="service">Truyền vào một bản ghi kiểu T</param>
        /// <param name="Id">Id của T muốn cập nhật </param>
        /// <returns>số bản ghi được cập nhật </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<int> UpdateService(TUpdateDto service, Guid Id);
        /// <summary>
        /// Tên hàm: nghiệp vụ liệt kê ra những thằng kiểu T 
        /// </summary>
        /// <returnsdanh sách các bản ghi </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        ///  
        //Task<IEnumerable<TDto>> FindAll();
        /// <summary>
        /// Tên hàm: tìm bản ghi kiểu T theo Id
        /// </summary>
        /// <param name="Id">Id của T muốn tìm </param>
        /// <returns>true: trả về T
        /// false: trả về null </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        //Task<TDto> FindOne(Guid id);

        

    }
}
