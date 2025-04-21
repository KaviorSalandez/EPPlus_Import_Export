using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class Account : IEntity
    {
        // ID của Người dùng 

        public Guid AccountId { get; set; }
        // Tên 
        public string UserName { get; set; }// Tên đẩy đủ 
        public string FullName { get; set; }

        // Email
        public string? Email { get; set; }

        // Điện thoại di động
        public string? PhoneNumber { get; set; }
        // mật khẩu
        public string? Password { get; set; }
        // vai trò user 

        public string? RoleName { get; set; }
    }
}
