using IncomeAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IncomeAPI.Services
{
    public class IncomeBackgroundService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScopeFactory _scopeFactory;

        public IncomeBackgroundService(IServiceProvider serviceProvider, IServiceScopeFactory scopeFactory)
        {
            _serviceProvider = serviceProvider;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Run the DoWork method every 5 seconds for testing
            /*  var timeUntilFirstRun = TimeSpan.FromSeconds(5);
              _timer = new Timer(DoWork, null, timeUntilFirstRun, TimeSpan.FromSeconds(5));
  */

            TimeSpan timeOfDayToRun = new TimeSpan(8, 0, 0); // 8:00 AM
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimeSpan timeUntilFirstRun;

            if (currentTime < timeOfDayToRun)
            {
                // If the current time is before the target time today
                timeUntilFirstRun = timeOfDayToRun - currentTime;
            }
            else
            {
                // If the current time is after the target time today, schedule for the next day
                timeUntilFirstRun = (timeOfDayToRun + TimeSpan.FromDays(1)) - currentTime;
            }

            // Set the timer to run DoWork method daily
            _timer = new Timer(DoWork, null, timeUntilFirstRun, TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                Console.WriteLine("Do work");
                var incomeRepository = scope.ServiceProvider.GetRequiredService<IIncomeRepository>();
                var produceMessage = scope.ServiceProvider.GetRequiredService<IProduceMessage>();

                var allIncomes = incomeRepository.GetAllMainIncome().GetAwaiter().GetResult();
                foreach (var income in allIncomes)
                {
                    if (income.Name == "salary")
                    {
                        produceMessage.ProduceMessageAsync(income.Amount.ToString()).GetAwaiter().GetResult();
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
