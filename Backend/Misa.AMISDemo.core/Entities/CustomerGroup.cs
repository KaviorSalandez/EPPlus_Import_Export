using MISA.AMISDemo.Core.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    // Nhóm Khách hàng 
    // created by: khanhddq
    // created at: 1/20/2023
    public class CustomerGroup : IEntity
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

        public override string ToString()
        {
            return "Customer group name : " + CustomerGroupName  + CustomerGroupId;
        }
    }
}
