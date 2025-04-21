using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Departments
{
    // interface chứa xử lý của department 
    // created by: khanhddq
    // created at: 1/20/2023
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        /// <summary>
        /// Tên hàm: tìm bản ghi theo tên văn phòng 
        /// </summary>
        /// <param name="DepartmentName">tên văn phòng muốn tìm  </param>
        /// <returns>true: trả về văn phòng
        /// false: trả về null </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        public Task<Department> CheckDepartmentName(String DepartmentName);
    }
}
