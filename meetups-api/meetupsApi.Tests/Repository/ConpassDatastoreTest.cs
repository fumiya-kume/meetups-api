using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace meetupsApi.Tests.Repository
{
    public class ConpassDatastoreTest
    {
        [Fact]
        async void Connpassから受信したデータをパースすることができる()
        {
            var client = new ConnpassDatastore();
            var connpassData = await client.LoadConnpassDataAsync();
            Assert.NotNull(connpassData);
        }

        [Fact]
        async void Connpassの件数を指定してデータを取得することができる()
        {
            var client = new ConnpassDatastore();
            var connpassData = await client.LoadConnpassDataAsync(99);
            Assert.Equal(99, connpassData.events.Length);
        }

    }
}
