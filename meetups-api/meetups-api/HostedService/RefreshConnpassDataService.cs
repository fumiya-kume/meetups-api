using System;
using System.Threading;
using System.Threading.Tasks;
using meetupsApi.Tests.Domain.Usecase;
using Microsoft.Extensions.Hosting;

namespace meetupsApi.HostedService
{
    public class RefreshConnpassDataService: IHostedService, IDisposable
    {
        private  Timer _timer;
        private IRefreshConnpassDataUsecase _refreshConnpassDataUsecase;

        public RefreshConnpassDataService(
            IRefreshConnpassDataUsecase refreshConnpassDataUsecase
            )
        {
            _refreshConnpassDataUsecase = refreshConnpassDataUsecase;
        }

        private void DoWork(object state)
        {
            _refreshConnpassDataUsecase.execute();
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(
                DoWork,
                null,
                TimeSpan.Zero, 
                TimeSpan.FromMinutes(30));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}