using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs.Accounts
{
    public class AccountDto
    {
        // ID của Người dùng 

        public Guid AccountId { get; set; }

        // Tên đầy đủ của người dùng 
        public string FullName { get; set; }
        // email người dùng 
        public string Email { get; set; }
    }
}
