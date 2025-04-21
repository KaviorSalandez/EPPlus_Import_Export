using MISA.AMISDemo.Core.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class Department : IEntity
    {
        // ID của phòng ban
     
        public Guid DepartmentId { get; set; }

        [Required(ErrorMessage = MISAConst.ERRMSG_DepartmentName)]
        [MinLength(5, ErrorMessage = MISAConst.ERRMSG_MinLength_Code)]
        [MaxLength(200, ErrorMessage = MISAConst.ERRMSG_MaxLength_Address)]
        // Tên phòng ban
        public string DepartmentName { get; set; }

      
     
    }
}
