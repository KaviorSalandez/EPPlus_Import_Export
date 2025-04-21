using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.DTOs.Accounts;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Accounts;
using MISA.AMISDemo.Core.Services;
using MISA.AMISDemo.Core.Interfaces.Employees;
using MISA.AMISDemo.Core.DTOs.Tokens;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsController : BaseReadOnlyController<AccountDto>
    {
        IAccountService _accountService;
        public AccountsController(IAccountService accountService) : base(accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Login")]
        /// <summary>
        /// Tên hàm: Đăng nhập 
        /// </summary>
        /// <param name="email">email đăng nhập </param>
        /// <param name="password">mật khẩu đăng nhập </param>
        /// <returns>trả về nhân viên đó </returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/1/20 
        public async Task<IActionResult> SignIn([FromBody] LoginRequest loginRequest)
        {
            var account  = await _accountService.SignIn(loginRequest.Username, loginRequest.Password);

            return StatusCode(200, account);

        }
        [HttpPost("RefreshToken")]
        /// <summary>
        /// Tên hàm: làm mới lại token 
        /// </summary>
        /// <param name="tokenDto">Đưa những thuộc tính của class tokenDto </param>
        /// <returns>Token Dto </returns>
        public IActionResult RefreshToken([FromBody] TokenDto tokenDto)
        {
            var account = _accountService.RefreshToken(tokenDto);

            return StatusCode(200, account);

        }
    }
}
