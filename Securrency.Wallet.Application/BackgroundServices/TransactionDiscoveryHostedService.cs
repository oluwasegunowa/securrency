using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SecurrencyTDS.Domain.Authorization;
using SecurrencyTDS.WalletManager.Application.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurrencyTDS.WalletManager.Application.BackgroundServices
{
    public class TransactionDiscoveryHostedService : IHostedService, IDisposable
    {
        private struct State
        {
            public static int numberOfActiveJobs = 0;
            public const int maxNumberOfActiveJobs = 1;
        }
        private int executionCount = 0;
        private readonly ILogger<TransactionDiscoveryHostedService> _logger;
        private readonly IOptions<StellarSettings> _settings;

        private Timer _timer;

        public IServiceScopeFactory _serviceScopeFactory;
        public TransactionDiscoveryHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<TransactionDiscoveryHostedService> logger, IOptions<StellarSettings> settings)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _settings = settings;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Hosted transaction discovery Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,  TimeSpan.FromSeconds(_settings.Value.TDSSynchronizationTimeInSec)); //This is to space out the execution time

            return Task.CompletedTask;
        }

        private void  DoWork(object state)
        {

            // allow only a certain number of concurrent work. In this case, 
            // only allow one job to run at a time. 
            if (State.numberOfActiveJobs < State.maxNumberOfActiveJobs)
            {
                // Update number of running jobs in one atomic operation. 
                try
                {
                    Interlocked.Increment(ref State.numberOfActiveJobs);


                    using (var scope = _serviceScopeFactory.CreateScope())
                    {

                        var mediatr = scope.ServiceProvider.GetRequiredService<IMediator>();

                      _ = mediatr.Send(new DiscoverWalletTransactionsCommand() { }).Result;

                    }
                }
                finally
                {
                    Interlocked.Decrement(ref State.numberOfActiveJobs);
                }
            }
            else
            {
                _logger.LogDebug("Job skipped since max number of active processes reached.");
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Hosted transaction discovery Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
