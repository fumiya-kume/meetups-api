using System;
using System.Collections.Generic;
using System.Text;
using meetupsApi.Tests.Domain.Repository;
using Xunit;

namespace meetupsApi.Tests.Repository
{
    public class ConpassDatastoreTest
    {
        [Fact(Skip = "CIでは実行しない")]
        async void Connpassから受信したデータをパースすることができる()
        {
            var client = new ConnpassDatastore();
            var connpassData = await client.LoadConnpassDataAsync();
            Assert.NotNull(connpassData);
        }

        [Fact(Skip = "CIでは実行しない")]
        async void Connpassの件数を指定してデータを取得することができる()
        {
            var client = new ConnpassDatastore();
            var connpassData = await client.LoadConnpassDataAsync(99);
            Assert.Equal(99, connpassData.ConnpassEvents.Length);
        }

        [Fact(Skip = "CIでは実行しない")]
        void ConnpassDataStoreはIConnpassDataStoreを継承している()
        {
            var target = new ConnpassDatastore();
            Assert.True(target is IConnpassDataStore);
        }
    }
}