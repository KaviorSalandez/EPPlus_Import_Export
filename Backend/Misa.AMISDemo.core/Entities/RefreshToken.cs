using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class RefreshToken
    {
        // unique id cho refresh token 
        public Guid RefreshTokenId { get; set; }

        // user id là cái mà refresh token đã được tạo 
        public Guid AccountId { get; set; }
        
        // JwtTokenId sẽ là token id duy nhất được cho tới access token 
        public string JwtTokenId { get; set; }

        // mã refresh 

        public string RefreshTokens { get; set; }

        // chắc chắn rằng chỉ có một mã refresh hợp lệ  
        public bool IsValid { get; set; }

        public DateTime ExpiresAt { get; set; }

    }
}
