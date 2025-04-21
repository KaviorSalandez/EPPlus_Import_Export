using MISA.AMISDemo.Core.DTOs.Tokens;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Accounts 
{
    public  interface IAccountRepository : IBaseRepository<Account>
    {
        /// <summary>
        /// Tên hàm: Đăng nhập tài khoản 
        /// </summary>
        ///  <param name="username">tên đăng nhập tài khoản </param>
        ///  <param name="password">mật khẩu tài khoản </param>
        /// <returns>true: trả thực thể account, token, và refreshtoken 
        /// false: trả về null </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2024/3/5 
        public Task<Account> SignIn(string username, string password);

        /// <summary>
        /// Tên hàm: Insert vào database refresh token  
        /// </summary>
        /// <param name="RefreshToken">Truyèn vào thực thể refreshToken </param>
        /// <returns>số bản ghi được tạo </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2024/3/5 
        public Task<int> RefreshToken(RefreshToken RefreshToken);

        /// <summary>
        /// Tên hàm: Update vào database refresh token  
        /// </summary>
        /// <param name="RefreshToken">Truyèn vào thực thể refreshToken</param>
        /// <param name="Id">Id của refreshToken muốn cập nhật </param>
        /// <returns>số bản ghi được cập nhật  </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2024/3/5 
        public Task<int> UpdateRefreshToken(RefreshToken RefreshToken, Guid Id);
        /// <summary>
        /// Tên hàm: Tìm refresh token 
        /// </summary>
        /// <param name="refreshTokenn">mã refresh token  </param>
        /// <returns>True: trả về Refresh token
        /// false trả về null </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2024/3/5 
        public Task<RefreshToken> FindRefreshToken (string refreshTokenn );
        /// <summary>
        /// Tên hàm: Tìm refresh token bằng jwt token và accountId 
        /// </summary>
        /// <param name="AccountId">tid tài khoản  </param>
        /// <param name="jwtToken">mã chung của refresh token và accountId  </param>
        /// <returns>True: trả về danh sách Refresh token
        /// false trả về null </returns>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2024/3/5 
        public Task<IEnumerable<RefreshToken>> FindRefreshTokenBỵJA(Guid AccountId, string jwtToken );
    }
}
