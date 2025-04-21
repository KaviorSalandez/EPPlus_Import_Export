using MISA.AMISDemo.Core.Const;
using MISA.AMISDemo.Core.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.AMISDemo.Core.Enum.MISAEnum;

namespace MISA.AMISDemo.Core.Entities
{
    // Khách hàng 
    // created by: khanhddq
    // created at: 1/20/2023
    public class Customer : IEntity
    {
        /// <summary>
        /// id của khách hàng 
        /// </summary>
        [Required]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// mã khách hàng bắt buộc và 5,100 ký tự 
        /// </summary>
        [Required (ErrorMessage =MISAConst.ERRMSG_CustomerCode)]
        [MinLength(5,ErrorMessage = MISAConst.ERRMSG_MinLength_Code)]
        [MaxLength(100, ErrorMessage = MISAConst.ERRMSG_MaxLength_Code)]
        public string CustomerCode { get; set; }
        /// <summary>
        /// tên đẩy đủ của khách hàng từ 5, 100 ký tự 
        /// </summary>
        [MinLength(5, ErrorMessage = MISAConst.ERRMSG_MinLength_Code)]
        [MaxLength(100, ErrorMessage = MISAConst.ERRMSG_MaxLength_Code)]
        public string Fullname { get; set; }

        /// <summary>
        /// Email nhận tin nhắn < 100 ký tự 
        /// </summary>
    
        [MaxLength(100, ErrorMessage = MISAConst.ERRMSG_MaxLength_Code)]

        [EmailAddress(ErrorMessage ="Email không đúng định dạng ")]
        public string Email { get; set; }

        /// <summary>
        /// Mã nhóm của khách hàng 
        /// </summary>

  
        public Guid? CustomerGroupId { get; set; }
        public string? CustomerGroupName { get; set; }
        /// <summary>
        /// số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Giới tính của khách hàng. 0 là nam, 1 là nữ, 2 la giới tính khác
        /// </summary>
        [Range(0,2)]
        // muốn sửa giá trị của gender thì vào kia 
        public Gender? Gender { get; set; }
        /// <summary>
        /// Địa chỉ cư chú của khách hàng < 255 ký tự 
        /// </summary>

        [MaxLength(255, ErrorMessage = MISAConst.ERRMSG_MaxLength_Address)]
        public string? Address { get; set; }

        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Tạo class validation , ngày tạo ra bản ghi mới này 
        /// </summary>
        /// 
        [CustomerGreaterToday(ErrorMessage = MISAConst.ERRMSG_DateCreate)]
        public DateTime? CreatedDate { get; set; }

 

    }
}
