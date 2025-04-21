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
    public class CustomerDto
    {
        /// <summary>
        /// id của khách hàng 
        /// </summary>
        [Required]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// mã khách hàng bắt buộc và 5,100 ký tự 
        /// </summary>
        [Required(ErrorMessage = MISAConst.ERRMSG_CustomerCode)]
        [MinLength(5, ErrorMessage = "Độ dài phải lớn hơn hoặc bằng 5 ký tự")]
        [MaxLength(100, ErrorMessage = "Không được vượt quá 100 ký tự")]
        public string CustomerCode { get; set; }
        /// <summary>
        /// tên đẩy đủ của khách hàng từ 5, 100 ký tự 
        /// </summary>
        [MinLength(5, ErrorMessage = "Độ dài phải lớn hơn hoặc bằng 5 ký tự")]
        [MaxLength(100, ErrorMessage = "Không được vượt quá 100 ký tự")]
        public string Fullname { get; set; }

        /// <summary>
        /// Email nhận tin nhắn < 100 ký tự 
        /// </summary>
        [MaxLength(100, ErrorMessage = "Không được vượt quá 100 ký tự")]

        [EmailAddress(ErrorMessage = "Email không đúng định dạng ")]
        public string Email { get; set; }

        /// <summary>
        /// Mã nhóm của khách hàng 
        /// </summary>

        [ForeignKey("CustomerGroupId")]
        public Guid? CustomerGroupId { get; set; }
        public string? CustomerGroupName { get; set; }
        /// <summary>
        /// số điện thoại bắt buộc và < 50 ký tự và validate dữ liệu
        /// </summary>

        [MaxLength(50, ErrorMessage = "Không được vượt quá 50 ký tự")]
        [Required(ErrorMessage = "Mobile no. is required")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Giới tính của khách hàng. 0 là nam, 1 là nữ, 2 la giới tính khác
        /// </summary>
        [Range(0, 2)]
        // muốn sửa giá trị của gender thì vào kia 
        public Gender? Gender { get; set; }
        /// <summary>
        /// Địa chỉ cư chú của khách hàng < 255 ký tự 
        /// </summary>

        [MaxLength(255, ErrorMessage = "Không được vượt quá 255 ký tự")]
        public string? Address { get; set; }
        //public string GenderName
        //{
        //    get
        //    {
        //        switch (Gender)
        //        {
        //            case Enum.MISAEnum.Gender.MALE:
        //                return "Nam";
        //            case Enum.MISAEnum.Gender.FEMALE:
        //                return "Nữ ";
        //            case Enum.MISAEnum.Gender.OTHER:
        //                return "Khác";
        //            default:
        //                return "Unknown";

        //        }
        //    }
        //}

    }
}
