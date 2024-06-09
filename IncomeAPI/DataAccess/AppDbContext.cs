using IncomeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeAPI.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MainIncome> MainIncomes { get; set; }
        public DbSet<AdditionalIncome> AdditionalIncomes { get; set; }
    }
}
