using BudgetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetAPI.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Budget> Budgets { get; set; }

    }
}
