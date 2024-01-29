using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using TwitterApi.Infrastructure.Entities.General;
using TwitterApi.Infrastructure.Entities.Mentions;
using TwitterApi.Infrastructure.Entities.Others;
using TwitterApi.Infrastructure.Entities.Users;
using TwitterApi.Infrastructure.Repositories.Base;
using static System.Net.Mime.MediaTypeNames;

namespace TwitterApi.Infrastructure.Database
{
    public class DBContext : DbContext, IUnitOfWork
    {
        private IDbContextTransaction currentTransaction;
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        #region Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Trend> Trends { get; set; }
        public DbSet<Mention> Mentions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LeftMenu> LeftMenus { get; set; }
        #endregion

        #region Transactions

        public IDbContextTransaction GetCurrentTransaction() => currentTransaction;
        public bool HasActiveTransaction => currentTransaction != null;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (currentTransaction != null) return null;
            currentTransaction = await Database.BeginTransactionAsync();
            return currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollBackTransaction();
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        public void RollBackTransaction()
        {
            try
            {
                currentTransaction?.Rollback();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>()
    .HasOne(c => c.User)
    .WithMany(u => u.Comments)
    .IsRequired()
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mention>()
                .HasOne(t => t.User)
                .WithMany(u => u.Mentions)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
