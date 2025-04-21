using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        DbConnection Connection { get; }

        DbTransaction? Transaction { get; }
        // bắt đầu mở ra
        void BeginTransaction();
        Task BeginTransactionAsync();
        // sau đó commit 
        void Commit();
        Task CommitAsync();
        // rồi quay lại khi có lỗi xảy ra 
        void Rollback();
        Task RollbackAsync();
    }
}
