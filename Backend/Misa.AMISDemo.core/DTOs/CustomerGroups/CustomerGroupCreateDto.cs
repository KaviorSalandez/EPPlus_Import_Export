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
using MISA.AMISDemo.Core.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MISA.AMISDemo.Core.DTOs.Customers
{
    // Khách hàng 
    // created by: khanhddq
    // created at: 1/20/2023
    public class CustomerGroupCreateDto
    {
        /// <summary>
        /// mã nhóm khách hàng,
        /// </summary>

        public Guid CustomerGroupId { get; set; }
        /// <summary>
        /// tên nhóm khách hàng là bắt buộc và từ 5,100 ký tự 
        /// </summary>
        [Required(ErrorMessage = MISAConst.ERRMSG_CustomerGroupNameCode)]
        [MinLength(5, ErrorMessage = MISAConst.ERRMSG_MinLength_Code)]
        [MaxLength(100, ErrorMessage = MISAConst.ERRMSG_MaxLength_Code)]
        public string CustomerGroupName { get; set; }


    }
}
