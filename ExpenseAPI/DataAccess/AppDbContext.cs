using ExpenseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseAPI.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FuturePlannedExpense> FuturePlannedExpenses { get; set; }
        public DbSet<DailyExpense> DailyExpenses { get; set; }
    }
}
