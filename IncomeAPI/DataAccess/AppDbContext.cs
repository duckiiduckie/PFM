using IncomeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeAPI.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; } 
        public DbSet<Category> Categories { get; set; }
    }
}
