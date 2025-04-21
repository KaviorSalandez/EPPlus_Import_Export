using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class IEntity
    {
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        // Người tạo
        public string? CreatedBy { get; set; }

        // Ngày sửa đổi
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        // Người sửa đổi
        public string? ModifiedBy { get; set; }
    }
}
