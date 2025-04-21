using MISA.AMISDemo.Core.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.AMISDemo.Core.Enum.MISAEnum;

namespace MISA.AMISDemo.Core.Entities
{
    public class Employee : IEntity
    {
        // ID của nhân viên

        public Guid EmployeeId { get; set; }
        // Tên Nhân Viên 
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = MISAConst.ERRMSG_DepartmentId)]
        // ID của phòng ban
        public Guid DepartmentId { get; set; }
        // Tên phòng ban 
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = MISAConst.ERRMSG_PositionId)]
        // ID của chức vụ
        public Guid PositionId { get; set; }

        // Tên chức vụ 
        public string PositionName { get; set; }
        // Mã nhân viên
        public string EmployeeCode { get; set; }

        // Ngày sinh
        public DateTime? DOB { get; set; }

        // Giới tính
        public Gender? Gender { get; set; }

        // Số CMT/CCCD
        public string? IDNo { get; set; }

        // Ngày cấp CMT/CCCD
        public DateTime? IssueDate { get; set; }

        // Nơi cấp CMT/CCCD
        public string? IssuedBy { get; set; }
        [MaxLength(200, ErrorMessage = MISAConst.ERRMSG_MaxLength_Address)]
        // Địa chỉ
        public string? Address { get; set; }

        // Điện thoại di động
        public string? MobilePhone { get; set; }

        // Điện thoại cố định
        public string? LandlinePhone { get; set; }

        // Email
        public string? Email { get; set; }

        // Số tài khoản ngân hàng
        public string? BankAccount { get; set; }

        // Tên ngân hàng
        public string? BankName { get; set; }
        [MaxLength(200, ErrorMessage = MISAConst.ERRMSG_MaxLength_Address)]
        // Chi nhánh ngân hàng
        public string? Branch { get; set; }
        // mật khẩu
        public string? Password { get; set; }

        // số bản ghi
        public int TotalRecord { get; set; }

    }
}
