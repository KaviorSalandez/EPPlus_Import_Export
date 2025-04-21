using MISA.AMISDemo.Core.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class Position : IEntity
    {
        // ID của chức vụ
        public Guid PositionId { get; set; }

        [Required(ErrorMessage =MISAConst.ERRMSG_PositionName)]
        [MinLength(5, ErrorMessage = MISAConst.ERRMSG_MinLength_Code)]
        [MaxLength(200, ErrorMessage = MISAConst.ERRMSG_MaxLength_Address)]
        // Tên chức vụ
        public string PositionName { get; set; }


       
    }
}
