using MISA.AMISDemo.Core.Const;
using MISA.AMISDemo.Core.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.AMISDemo.Core.Enum.MISAEnum;

namespace MISA.AMISDemo.Core.DTOs.Customers
{
    public class EmployeeExcelDto
    {
        // Mã nhân viên
        public string EmployeeCode { get; set; }
        // Tên Nhân Viên 
        public String EmployeeName { get; set; }
        [Required(ErrorMessage = MISAConst.ERRMSG_DepartmentId)]


        // Giới tính
        public Gender? Gender { get; set; }


        // Ngày sinh
        public DateTime? DOB { get; set; }

        // Tên chức vụ 
        public string PositionName { get; set; }

        [Required(ErrorMessage = MISAConst.ERRMSG_EmployeeCode)]
        // Tên phòng ban 
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = MISAConst.ERRMSG_PositionId)]
        

        // Số tài khoản ngân hàng
        public string? BankAccount { get; set; }

        // Tên ngân hàng
        public string? BankName { get; set; }
        

    }
}
