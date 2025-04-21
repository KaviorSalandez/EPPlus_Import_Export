using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Positions
{
    // interface chứa xử lý của vị trí trong công ty
    // created by: khanhddq
    // created at: 1/20/2023
    public interface IPositionRepository : IBaseRepository<Position>
    {
        /// <summary>
        /// tìm bản ghi theo vị trí tên  
        /// </summary>
        /// <param name="PositionName">tên vị trí muốn tìm  </param>
        /// <returns>true: trả về vị trí
        /// false: trả về null </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        public Task<Position> CheckPositionName(String PositionName);
    }
}
