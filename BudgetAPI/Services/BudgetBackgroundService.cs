using BudgetAPI.Models.Dto;
using BudgetAPI.Repositories;

namespace BudgetAPI.Services
{
    public class BudgetBackgroundService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;

        public BudgetBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Schedule the task to run at the beginning of the next month
            var now = DateTime.Now;
            var firstOfNextMonth = new DateTime(now.Year, now.Month, 1).AddMonths(1);
            var timeUntilFirstOfNextMonth = firstOfNextMonth - now;

            _timer = new Timer(DoWork, null, timeUntilFirstOfNextMonth, TimeSpan.FromDays(30));

          /*  var timeUntilFirstRun = TimeSpan.FromSeconds(5);

            _timer = new Timer(DoWork, null, timeUntilFirstRun, TimeSpan.FromSeconds(5));*/

            return Task.CompletedTask;

        }

        private void DoWork(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var budgetRepository = scope.ServiceProvider.GetRequiredService<IBudgetRepository>();

                var allBudgets = budgetRepository.GetAllBudgets().GetAwaiter().GetResult();
                var userIds = allBudgets.Select(b => b.UserId).Distinct().ToList();

                foreach (var userId in userIds)
                {
                    var latestBudget = allBudgets.Where(b => b.UserId == userId).OrderByDescending(b => b.Date).FirstOrDefault();

                    if (latestBudget != null)
                    {
                        var newBudget = new CreateBudget
                        {
                            UserId = userId,
                            Amount = latestBudget.Amount,
                            Date = DateTime.Now,
                            UserEmail = latestBudget.UserEmail,
                            Type = latestBudget.Type,
                            IsMailSent = false,
                            Essential = latestBudget.Essential,
                            Want = latestBudget.Want,
                            SavingAndInvestment = latestBudget.SavingAndInvestment
                        };
                        budgetRepository.CreateBudget(newBudget).GetAwaiter().GetResult();
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
