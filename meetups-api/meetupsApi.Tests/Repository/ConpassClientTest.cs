using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using meetupsApi.JsonEntity;
using Newtonsoft.Json;
using Xunit;

namespace meetupsApi.Tests.Repository
{
    public class ConpassClientTest
    {
        [Fact]
        void Connpassにアクセスするクラスが存在する()
        {
            var client = new ConnpassClient();
            Assert.NotNull(client);
        }

        [Fact]
        async void ConnpassからJsonを取得することができる()
        {
            var client = new ConnpassClient();
            var json = await client.loadJsonAwait();
            Assert.NotNull(json);
            Assert.NotEmpty(json);
        }

        [Fact]
        async void Connpassから受信したデータをパースすることができる()
        {
            var client = new ConnpassClient();
            var connpassData = await client.LoadConnpassDataAsync();
            Assert.NotNull(connpassData);
        }

        [Fact]
        async void Connpassの件数を指定してデータを取得することができる()
        {
            var client = new ConnpassClient();
            var connpassData = await client.LoadConnpassDataAsync(99);
            Assert.Equal(99, connpassData.events.Length);
        }

    }

    public class ConnpassClient
    {
        public async Task<string> loadJsonAwait(int capacity = 100)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://connpass.com/api/v1/event/?count={capacity}");
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<ConnpassMeetupJson> LoadConnpassDataAsync(int capacity = 100)
        {
            var json = await loadJsonAwait(capacity);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ConnpassMeetupJson>(json);
        }
    }
}
