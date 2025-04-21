using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs.Accounts
{
    public class LoginResponseDTO
    {
        public AccountDto Account  { get; set; }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public LoginResponseDTO(AccountDto user, string token, string refreshToken)
        {
            Account = user;
            AccessToken = token;
            RefreshToken = refreshToken;
        }


    }
}
