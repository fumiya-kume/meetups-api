using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace meetupsApi.Tests.Repository
{
    public class ConpassClientTest
    {
        [Fact]
        void Connpassにアクセスするクラスが存在する()
        {
            var client = new ConpassClient();
            Assert.NotNull(client);
        }

        [Fact]
        async void ConnpassからJsonを取得することができる()
        {
            var client = new ConpassClient();
            var json = await client.loadJsonAwait();
            Assert.NotNull(json);
        }
    }

    public class ConpassClient
    {
        public async Task<object> loadJsonAwait()
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync("https://connpass.com/api/v1/event/");
            }
        }
    }
}
