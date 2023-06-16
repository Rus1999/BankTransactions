using Microsoft.EntityFrameworkCore;

namespace BankTransactions.Models
{
    public class TransactionsDbContext: DbContext
    {
        public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options) : base(options)
        { }

        public DbSet<Transactions> Transactions { get; set; }
    }
}
