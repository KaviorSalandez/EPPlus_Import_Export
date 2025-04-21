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
    public class PositionCreateDto
    {
        // ID của chức vụ
        public Guid PositionId { get; set; }

        [Required(ErrorMessage = MISAConst.ERRMSG_PositionName)]
        [MinLength(5, ErrorMessage = MISAConst.ERRMSG_MinLength_Code)]
        [MaxLength(200, ErrorMessage = MISAConst.ERRMSG_MaxLength_Address)]
        // Tên chức vụ
        public string PositionName { get; set; }

        // Ngày tạo

    }
}
