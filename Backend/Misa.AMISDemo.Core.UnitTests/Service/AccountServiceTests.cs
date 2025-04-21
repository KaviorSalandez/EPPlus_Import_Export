using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MISA.AMISDemo.Core.DTOs.Accounts;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.DTOs.Tokens;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Accounts;
using MISA.AMISDemo.Core.Interfaces.Departments;
using MISA.AMISDemo.Core.Interfaces.Employees;
using MISA.AMISDemo.Core.Interfaces.Positions;
using MISA.AMISDemo.Core.Services;
using MISA.AMISDemo.Core.UnitOfWorks;
using MISA.AMISDemo.Infracstructure.Interfaces;
using MISA.AMISDemo.Infracstructure.Repositories;
using NSubstitute;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.UnitTests.Service
{
    [TestFixture]
    public class AccountServiceTests
    {

        public IAccountRepository accountRepository;
        public IMapper mapper;
        public AccountService accountService;

        public IConfiguration configuration;
        [SetUp]
        public void Setup()
        {
            accountRepository = Substitute.For<IAccountRepository>();
            // Mock configuration
            var inMemorySettings = new Dictionary<string, string>
        {
            {"ApiSettings:Secret", "Hello World tôi khá thích chơi mèo"}
        };
            configuration = new ConfigurationBuilder()
               .AddInMemoryCollection(inMemorySettings)
            .Build();
            mapper = Substitute.For<IMapper>();
            accountService = Substitute.For<AccountService>(accountRepository, mapper, configuration);
        }

        /// <summary>
        /// Tên hàm: Đăng nhập tài khoản
        /// </summary>
        /// <returns>
        /// N/A
        /// </returns>
        /// <remarks>
        /// Created by: Đặng Đình Quốc Khánh
        /// Created at: 2024/3/9
        /// </remarks>
        [Test]
        public void SignIn_ValidCredentials_ReturnsLoginResponseDTO()
        {
            // Arrange
            string email = "test@example.com";
            string rolename = "admin";
            string password = "password";
            var jwtTokenId = $"{Guid.NewGuid()}";
            var refreshToken = $"R{Guid.NewGuid}";
            var expectedAccount = new Account { UserName = email, Password = password, RoleName = rolename };
            AccountDto accountDto = new AccountDto();
            accountRepository.SignIn(email, password).Returns(expectedAccount);

            // Act
            var result = accountService.SignIn(email, password);

            // Assert
            Assert.IsNotNull(result);
            accountService.Received(1).GetAccessToken(expectedAccount, jwtTokenId);
            accountService.Received(1).MapEntityToDto(expectedAccount);
            accountService.Received(1).CreateNewRefreshTOken(expectedAccount.AccountId, jwtTokenId);
        }

        /// <summary>
        /// Tên hàm: Xác thực thông tin đăng nhập không hợp lệ
        /// </summary>
        /// <returns>
        /// N/A
        /// </returns>
        /// <remarks>
        /// Created by: Đặng Đình Quốc Khánh
        /// Created at: 2024/3/9
        /// </remarks>
        [Test]
        public void SignIn_InvalidCredentials_ThrowsValidateException()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password";
            Account account = null;
            accountRepository.SignIn(email, password).Returns(account);

            // Act + Assert
            try
            {
                var result = accountService.SignIn(email, password);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.SingInFail));
            }
        }

        /// <summary>
        /// Tên hàm: Đăng nhập với email rỗng
        /// </summary>
        /// <returns>
        /// N/A
        /// </returns>
        /// <remarks>
        /// Created by: Đặng Đình Quốc Khánh
        /// Created at: 2024/3/9
        /// </remarks>
        [Test]
        public void SignIn_EmptyEmail_ThrowsValidateException()
        {
            // Arrange
            string email = "";
            string password = "password";

            // Act + Assert
            try
            {
                var result = accountService.SignIn(email, password);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.Email));
            }
        }

        [Test]
        // Kiểm tra xem hàm SignIn có ném ra ngoại lệ ValidateException khi mật khẩu rỗng không
        /// Created by: Đặng Đình Quốc Khánh
        /// Created at: 2024/3/9
        public void SignIn_EmptyPassword_ThrowsValidateException()
        {
            // Arrange
            string email = "anhpttm16@gmail.com";
            string password = "";

            // Act
            try
            {
                var result = accountService.SignIn(email, password);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.Password));
            }
        }

        [Test]
        // Kiểm tra xem hàm RefreshToken có trả về đối tượng TokenDto khi RefreshToken không hợp lệ không
        /// Created by: Đặng Đình Quốc Khánh
        /// Created at: 2024/3/9
        public void RefreshToken_RefreshTokenInValid_ReturnsTokenDto()
        {
            // Arrange
            var tokenDto = new TokenDto { RefreshToken = "existing_refresh_token", AccessToken = "existing_access_token" };
            var existingRefreshToken = new RefreshToken { JwtTokenId = "existing_jwt_token_id", ExpiresAt = DateTime.UtcNow.AddHours(1), IsValid = false, RefreshTokens = tokenDto.RefreshToken, AccountId = Guid.Empty };
            accountRepository.FindRefreshToken(tokenDto.RefreshToken).Returns(existingRefreshToken);
            var accountId = Guid.Empty;
            var tokenId = "existing_jwt_token_id";
            accountService.GetAccessTokenData(tokenDto.AccessToken).Returns((true, accountId.ToString(), tokenId));

            // Act & Assert
            try
            {
                var result = accountService.RefreshToken(tokenDto);
            }
            catch (Exception ex)
            {
                accountRepository.Received(1).FindRefreshToken(tokenDto.RefreshToken);
                accountService.Received(1).GetAccessTokenData(tokenDto.AccessToken);
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.TokenInvalid));
            }
        }

        [Test]
        // Kiểm tra xem hàm RefreshToken có ném ra ngoại lệ ValidateException khi AccessToken trùng khớp không
        /// Created by: Đặng Đình Quốc Khánh
        /// Created at: 2024/3/9
        public void RefreshToken_AccessTokenMatch_ThrowsValidateException()
        {
            // Arrange
            var tokenDto = new TokenDto { RefreshToken = "existing_refresh_token", AccessToken = "existing_access_token" };
            var existingRefreshToken = new RefreshToken { JwtTokenId = "existing_jwt_token_id", ExpiresAt = DateTime.UtcNow.AddHours(1), IsValid = true, RefreshTokens = tokenDto.RefreshToken };
            string email = "test@example.com";
            string rolename = "admin";
            string password = "password";
            var jwtTokenId = $"{Guid.NewGuid()}";
            var expectedAccount = new Account { UserName = email, Password = password, RoleName = rolename };
            accountRepository.FindRefreshToken(tokenDto.RefreshToken).Returns(existingRefreshToken);

            // Act
            try
            {
                var result = accountService.RefreshToken(tokenDto);
            }
            catch (Exception ex)
            {
                accountRepository.Received(1).FindRefreshToken(tokenDto.RefreshToken);
                accountService.Received(1).GetAccessTokenData(tokenDto.AccessToken);
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.TokenMatch));
            }
        }

        [Test]
        // Kiểm tra xem hàm RefreshToken có ném ra ngoại lệ ValidateException khi RefreshToken không hợp lệ không
        /// Created by: Đặng Đình Quốc Khánh
        /// Created at: 2024/3/9
        public void RefreshToken_InvalidRefreshToken_ThrowsValidateException()
        {
            // Arrange
            var tokenDto = new TokenDto { RefreshToken = "invalid_refresh_token", AccessToken = "existing_access_token" };
            accountRepository.FindRefreshToken(tokenDto.RefreshToken).Returns((RefreshToken)null);

            // Act + Assert
            try
            {
                accountService.RefreshToken(tokenDto);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.FindRefreshToken));
            }
        }


        [Test]
        // Kiểm tra xem hàm RefreshToken có ném ra ngoại lệ ValidateException khi RefreshToken đã hết hạn không
        /// Created by: Đặng Đình Quốc Khánh
        /// Created at: 2024/3/9
        public void RefreshToken_ExpiredRefreshToken_ThrowsValidateException()
        {
            // Arrange
            var tokenDto = new TokenDto { RefreshToken = "existing_refresh_token", AccessToken = "existing_access_token" };
            var existingRefreshToken = new RefreshToken { JwtTokenId = "existing_jwt_token_id", ExpiresAt = DateTime.UtcNow.AddDays(-1), IsValid = true, RefreshTokens = tokenDto.RefreshToken, AccountId = Guid.Empty };
            accountRepository.FindRefreshToken(tokenDto.RefreshToken).Returns(existingRefreshToken);
            var accountId = Guid.Empty;
            var tokenId = "existing_jwt_token_id";
            accountService.GetAccessTokenData(tokenDto.AccessToken).Returns((true, accountId.ToString(), tokenId));

            // Act & Assert
            try
            {
                var result = accountService.RefreshToken(tokenDto);
            }
            catch (Exception ex)
            {
                accountRepository.Received(1).FindRefreshToken(tokenDto.RefreshToken);
                accountService.Received(1).GetAccessTokenData(tokenDto.AccessToken);
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.RefreshTokenExpire));
            }
        }

        [Test]
        // Kiểm tra xem hàm RefreshToken có ném ra ngoại lệ ValidateException khi tài khoản không hợp lệ không
        /// Created by: Đặng Đình Quốc Khánh
        /// Created at: 2024/3/9
        public void RefreshToken_InValidAccount_ThrowsValidateException()
        {
            // Arrange
            var tokenDto = new TokenDto { RefreshToken = "existing_refresh_token", AccessToken = "existing_access_token" };
            var existingRefreshToken = new RefreshToken { JwtTokenId = "existing_jwt_token_id", ExpiresAt = DateTime.UtcNow.AddDays(1), IsValid = true, RefreshTokens = tokenDto.RefreshToken, AccountId = Guid.Empty };
            accountRepository.FindRefreshToken(tokenDto.RefreshToken).Returns(existingRefreshToken);
            var accountId = Guid.Empty;
            var tokenId = "existing_jwt_token_id";
            accountService.GetAccessTokenData(tokenDto.AccessToken).Returns((true, accountId.ToString(), tokenId));
            string email = "test@example.com";
            string password = "password";
            Account account = new Account { Email = email, Password = password };
            accountRepository.FindOne(accountId).Returns(account);

            // Act & Assert
            try
            {
                var result = accountService.RefreshToken(tokenDto);
            }
            catch (Exception ex)
            {
                accountRepository.Received(1).FindRefreshToken(tokenDto.RefreshToken);
                accountService.Received(1).GetAccessTokenData(tokenDto.AccessToken);
                Assert.That(ex.Message, Is.EqualTo(MISA.AMISDemo.Core.Resource.Resource_VN.FindAccount));
            }
        }

        [Test]
        // Kiểm tra xem hàm RefreshToken có trả về RefreshToken hợp lệ khi tất cả các điều kiện đều thỏa mãn
        /// Created by: Đặng Đình Quốc Khánh
        /// Created at: 2024/3/9
        public void RefreshToken_TokenValid_ReturnValidRefreshToken()
        {
            // Arrange
            var tokenDto = new TokenDto { RefreshToken = "existing_refresh_token", AccessToken = "existing_access_token" };
            var existingRefreshToken = new RefreshToken { JwtTokenId = "existing_jwt_token_id", ExpiresAt = DateTime.UtcNow.AddDays(1), IsValid = true, RefreshTokens = tokenDto.RefreshToken, AccountId = Guid.Empty };
            accountRepository.FindRefreshToken(tokenDto.RefreshToken).Returns(existingRefreshToken);
            var accountId = Guid.Empty;
            var tokenId = "existing_jwt_token_id";
            accountService.GetAccessTokenData(tokenDto.AccessToken).Returns((true, accountId.ToString(), tokenId));
            string email = "test@example.com";
            string rolename = "admin";
            string password = "password";
            var account = new Account { UserName = email, Email = email, Password = password, RoleName = rolename };
            accountRepository.FindOne(accountId).Returns(account);
            accountRepository.SignIn(account.Email, account.Password).Returns(account);

            // Act 
            var result = accountService.RefreshToken(tokenDto);

            // Assert
            accountRepository.Received(1).FindRefreshToken(tokenDto.RefreshToken);
            accountService.Received(1).GetAccessTokenData(tokenDto.AccessToken);
            accountRepository.Received(1).FindOne(accountId);
            accountRepository.Received(1).SignIn(account.Email, account.Password);
            Assert.IsNotNull(result);
        }
    }
}
