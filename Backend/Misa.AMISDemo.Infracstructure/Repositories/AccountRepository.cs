using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using MISA.AMISDemo.Core.DTOs.Tokens;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Accounts;
using MISA.AMISDemo.Core.UnitOfWorks;
using MISA.AMISDemo.Infracstructure.Interfaces;
using MISA.AMISDemo.Infracstructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infracstructure.Repositories
{
    // ẻm này kế thừa từ base và dùng các hành động của cha nó là base
    // ẻm iAccount kia thì được hành động riêng. và tý nữa tiêm em nó 
    // 
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        IMISAdbContext _context;

        IUnitOfWork _uow;
        public AccountRepository(IMISAdbContext context, IUnitOfWork uow) : base(context)
        {
            _context = context;
            _uow = uow;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshToken)
        {
            var sql = $"SELECT * FROM RefreshToken where RefreshTokens = @refreshToken ";
            var param = new DynamicParameters();
            param.Add("refreshToken", refreshToken);
            var entity = await _uow.Connection.QueryFirstOrDefaultAsync<RefreshToken>(sql, param, transaction: _uow.Transaction);

            return entity;
        }

        public async Task<IEnumerable<RefreshToken>> FindRefreshTokenBỵJA(Guid accountId, string jwtToken)
        {
            var sql = $"SELECT * FROM RefreshToken where JwtTokenId = @JwtTokenId and AccountId = @accountId";
            var param = new DynamicParameters();
            param.Add("JwtTokenId", jwtToken);
            param.Add("accountId", accountId);
            var entity = await _uow.Connection.QueryAsync<RefreshToken>(sql, param, transaction: _uow.Transaction);

            return entity;
        }

        public async Task<int> RefreshToken(RefreshToken RefreshToken)
        {
            return await _context.CreateOne<RefreshToken>(RefreshToken);
       
        }

        public async Task<Account> SignIn(string username, string password)
        {
            var sql = $"SELECT * FROM Account u JOIN Role r on u.RoleId = r.RoleId  WHERE ( Username = @username or  Email  = @username or PhoneNumber = @username) and Password = @password ";
            var param = new DynamicParameters();
            param.Add("password", password);
            param.Add("username", username);
            var entity = await _uow.Connection.QueryFirstOrDefaultAsync<Account>(sql, param, transaction: _uow.Transaction);

            return entity;

        }

        public async Task<int> UpdateRefreshToken(RefreshToken refreshToken,Guid RefreshTokenId)
        {
            var sql = $"UPDATE RefreshToken SET IsValid = @IsValid WHERE RefreshTokenId = @RefreshTokenId";
            var param = new DynamicParameters();
            param.Add("RefreshTokenId", RefreshTokenId);
            param.Add("IsValid", refreshToken.IsValid);

            var affectedRows = await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction);
            return affectedRows;
        }
    }
}
