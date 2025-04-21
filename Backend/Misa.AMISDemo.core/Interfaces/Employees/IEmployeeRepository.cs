using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Employees
{
    /// <summary>
    /// Chứa các thao tác của employee khi thực hiện với database 
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Tên hàm: Check trùng mã nhân viên của nhan vien 
        /// </summary>
        /// <param name="employeeCode">truyền vào một mã nhan vien </param>
        /// <returns>true: trả thực thể nhan vien
        /// false: trả về null </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        public Task<Employee> CheckEmployeeCode(string employeeCode);

        /// <summary>
        /// Tên hàm: Check trùng bank account cua nhan vien 
        /// </summary>
        /// <param name="bankAccount">truyền vào một mã nhân viên </param>
        /// <returns>true: trả thực thể nhan vien
        /// false: trả về null </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        public Task<Employee> CheckBankAccount(string bankAccount);

        /// <summary>
        ///Tên hàm: Tìm nhiều bản ghi 
        /// </summary>
        /// <param name="Id">Id của thực thể muốn xóa </param>
        /// <returns>số bản ghi được xóa </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        Task<IEnumerable<Employee>> FindManyRecord(List<Guid> Ids);
    }
}