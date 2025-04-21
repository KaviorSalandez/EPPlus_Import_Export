using AutoMapper;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMISDemo.Core.DTOs.Accounts;
using Microsoft.Azure.Documents.SystemFunctions;
using Microsoft.IdentityModel.Tokens;
using MISA.AMISDemo.Core.Interfaces.Employees;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using MISA.AMISDemo.Core.DTOs.Tokens;
using MISA.AMISDemo.Core.Services.Base;

namespace MISA.AMISDemo.Core.Services
{
    /// <summary>
    /// service riêng của Account
    /// </summary>
    public class AccountService : BaseReadOnlyService<Account, AccountDto>, IAccountService
    {
        IAccountRepository _accountRepository;
        IMapper _mapper;
        public string secretKey;
        public AccountService(IAccountRepository AccountRepository, IMapper mapper, IConfiguration configuration) : base(AccountRepository)
        {
            _accountRepository = AccountRepository;
            secretKey = configuration.GetSection("ApiSettings")["Secret"];
            _mapper = mapper;
        }


        public override AccountDto MapEntityToDto(Account entity)
        {
            var entityDto = _mapper.Map<AccountDto>(entity);
            return entityDto;
        }

        

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
           
            return new TokenDto()
            {
                AccessToken = "hello",
                RefreshToken = "hello1",
            };
        }

        public async Task<LoginResponseDTO> SignIn(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.Email);
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.Password);
            }
            var account = await _accountRepository.SignIn(email, password);

            if (account == null)
            {
                throw new ValidateException(MISA.AMISDemo.Core.Resource.Resource_VN.SingInFail);
            }
            var jwtTokenId = $"JTI{Guid.NewGuid()}";
            string token = GetAccessToken(account,jwtTokenId);
            var accountDto = MapEntityToDto(account);
            var refreshToken = await CreateNewRefreshTOken(account.AccountId, jwtTokenId);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO(
              accountDto,token, refreshToken);
            return loginResponseDTO;
        }

        
        public async Task<string> CreateNewRefreshTOken(Guid accountId, string tokenId)
        {
            // tạo object chứa thuộc tính refresh token 
            RefreshToken refreshToken = new()
            { 
                RefreshTokenId = Guid.NewGuid(),
                IsValid = true,
                AccountId = accountId,
                JwtTokenId = tokenId,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                RefreshTokens = Guid.NewGuid() + "-" + Guid.NewGuid(),
            };
            Console.WriteLine(refreshToken);
            var refresh = await _accountRepository.RefreshToken(refreshToken);
            return refreshToken.RefreshTokens;
        }


        /// <summary>
        /// Tạo token mới 
        /// </summary>
        /// <param name="account">account muốn tạo token  </param>
        /// <param name="jwtTokenId"></param>
        /// <returns></returns>
        public string GetAccessToken(Account account, string jwtTokenId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            Console.WriteLine(secretKey);
            // mã hóa secretkey
            var key = Encoding.ASCII.GetBytes(secretKey);
            // kiểm tra  
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, account.RoleName),
                new Claim(JwtRegisteredClaimNames.Jti, jwtTokenId),
                new Claim(JwtRegisteredClaimNames.Sub,account.AccountId.ToString())
            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            
            var token =  tokenHandler.CreateToken(tokenDescriptor);
            //Console.WriteLine(token.ValidTo);
            string tokenStr =  tokenHandler.WriteToken(token);
            return tokenStr;
        }


        /// <summary>
        /// Làm mới lại token và thực hiện đăng nhập lại nếu refresh token cũng đã hết hạn.
        /// </summary>
        /// <param name="tokenDto">Thực thể tokenDto chứa thông tin cần thiết cho việc làm mới token.</param>
        /// <returns>
        /// true: Trả về thực thể TokenDto mới đã làm mới.
        /// false: Trả về null nếu không thể làm mới token hoặc cần đăng nhập lại.
        /// </returns>
        /// <remarks>
        /// Created by: Đặng Đình Quốc Khánh
        /// Created at: 2024/3/5
        /// </remarks>
        public virtual (bool isSuccessful, string accountId, string tokenId) GetAccessTokenData(string accessToken)
        {
            try
            {
                /* - đọc từ access token
                 * - tìm trong claims với cái mình đã lưu
                 * - trả về token data 
                 */
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(accessToken);
                var jwtTokenId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Jti).Value;
                var accountId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value;
                return (true, accountId, jwtTokenId);
            }
            catch (Exception)
            {

                return (false, null, null);
            }
        }
    }
}
