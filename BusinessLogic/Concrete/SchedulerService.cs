using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyProject.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyProject.BusinessLogic.Concrete
{
    public class SchedulerService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private Timer _timer;
        IMatchService _matchService;
        public SchedulerService(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _matchService.UpdateDB("03/08/2021");
            var count = Interlocked.Increment(ref executionCount);

        }

        public Task StopAsync(CancellationToken stoppingToken)
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
