using System;
using System.Collections.Generic;
using System.Text;
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
    }

    public class ConpassClient
    {

    }
}
