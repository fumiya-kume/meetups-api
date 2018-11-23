using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using meetupsApi.Tests.Domain.Usecase;
using Microsoft.Extensions.Hosting;

namespace meetupsApi.HostedService
{
    public class RefreshConnpassDataService : IHostedService, IDisposable
    {
        private Timer _timer;

        private async void DoWork(object state)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(5);
                var requestUrl = @"https://meetups-api.azurewebsites.net/batch";
                var content = new StringContent("");
                var result = await client.PostAsync(requestUrl, content);
            }
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