using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces
{
    public interface IBaseReadonlyService<TDto>
    {
        /// <summary>
        /// Tên hàm: Nghiệp vụ muốn xóa ở mỗi kiểu T 
        /// </summary>
        /// <param name="Id">Id của T muốn xóa </param>
        /// <returns>số bản ghi được xóa</returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<TDto> FindOneJoin(Guid Id);

        /// <summary>
        /// Tên hàm: nghiệp vụ liệt kê ra những thằng kiểu T 
        /// </summary>
        /// <returnsdanh sách các bản ghi </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        ///  
        Task<IEnumerable<TDto>> FindAll();
        /// <summary>
        /// Tên hàm: tìm bản ghi kiểu T theo Id
        /// </summary>
        /// <param name="Id">Id của T muốn tìm </param>
        /// <returns>true: trả về T
        /// false: trả về null </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<TDto> FindOne(Guid id);

    }
}
