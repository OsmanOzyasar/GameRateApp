using GameRateApp.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameRateAppDbContext _db;
        private IDbContextTransaction _transaction;
        public UnitOfWork(GameRateAppDbContext db)
        {
            _db = db;
        }

        public async Task BeginTransectionAsync()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransectionAsync()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task RollBackTransectionAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
