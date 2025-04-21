using MISA.AMISDemo.Core.DTOs.Accounts;
using MISA.AMISDemo.Core.DTOs.Tokens;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Accounts
{
    public interface IAccountService : IBaseReadonlyService<AccountDto>
    {
        /// <summary>
        /// Tên hàm: Đăng nhập tài khoản
        /// </summary>
        ///  <param name="email">email tài khoản</param>
        ///  <param name="password">mật khẩu tài khoản</param>
        /// <returns>true: trả thực thể account
        /// false: trả về null </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2024/3/5 
        public Task<LoginResponseDTO> SignIn(string email, string password);

        /// <summary>
        /// Tên hàm: làm mới lại token 
        /// nếu refresh token mà cũng hết hạn, thì login lại 
        /// </summary>
        /// <param name="tokenDto">Truyền thực thể tokenDto </param>
        /// <returns>true: trả thực thể TokenDto
        /// false: trả về null </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2024/3/5 
        public Task<TokenDto> RefreshToken(TokenDto tokenDto);

    }
}
