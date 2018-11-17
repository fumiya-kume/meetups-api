using System.Linq;
using meetupsApi.Domain.Entity;
using meetupsApi.Models;
using Xunit;

namespace meetupsApi.Tests.Helper
{
    public class InmemoryDbTestMockTests
    {
        [Fact]
        void DBにデータを保存することができる()
        {
            using (var testMock = new InmemoryDbTestMock<MeetupsApiContext>())
            {
                var context = testMock.Context();

                var entity = new ConnpassEventDataEntity();
                entity.EventTitle = "タイトル";
                entity.EventDescription = "デスク";
                entity.EventUrl = "www.yahoo.co.jp";
                entity.Lat = 1.2;
                entity.Lon = 1.3;
                context.ConnpassEventDataEntities.Add(entity);
                context.SaveChanges();

                Assert.Equal(1, context.ConnpassEventDataEntities.Count());
                testMock.Dispose();
            }
        }

        [Fact]
        void DBに連続して書き込みを行ってもDisposableな実装にしていると以前のデータベースは消えている()
        {
            using (var testMock = new InmemoryDbTestMock<MeetupsApiContext>())
            {
                var context = testMock.Context();

                var entity = new ConnpassEventDataEntity();
                entity.EventTitle = "タイトル";
                entity.EventDescription = "デスク";
                entity.EventUrl = "www.yahoo.co.jp";
                entity.Lat = 1.2;
                entity.Lon = 1.3;
                context.ConnpassEventDataEntities.Add(entity);
                context.SaveChanges();

                Assert.Equal(1, context.ConnpassEventDataEntities.Count());
            }

            using (var testMock = new InmemoryDbTestMock<MeetupsApiContext>())
            {
                var context = testMock.Context();

                var entity = new ConnpassEventDataEntity();
                entity.EventTitle = "タイトル";
                entity.EventDescription = "デスク";
                entity.EventUrl = "www.yahoo.co.jp";
                entity.Lat = 1.2;
                entity.Lon = 1.3;
                context.ConnpassEventDataEntities.Add(entity);
                context.SaveChanges();

                Assert.Equal(1, context.ConnpassEventDataEntities.Count());
            }
        }

        [Fact]
        void Disposableにしない場合は以前のデータが残っている()
        {
            using (var testMock = new InmemoryDbTestMock<MeetupsApiContext>())
            {
                var context = testMock.Context();

                var entity = new ConnpassEventDataEntity();
                entity.EventTitle = "タイトル";
                entity.EventDescription = "デスク";
                entity.EventUrl = "www.yahoo.co.jp";
                entity.Lat = 1.2;
                entity.Lon = 1.3;
                context.ConnpassEventDataEntities.Add(entity);
                context.SaveChanges();

                Assert.Equal(1, context.ConnpassEventDataEntities.Count());

                var mock2 = new InmemoryDbTestMock<MeetupsApiContext>();

                var context2 = mock2.Context();

                var entity2 = new ConnpassEventDataEntity();
                entity2.EventTitle = "タイトル";
                entity2.EventDescription = "デスク";
                entity2.EventUrl = "www.yahoo.co.jp";
                entity2.Lat = 1.2;
                entity2.Lon = 1.3;
                context.ConnpassEventDataEntities.Add(entity2);
                context.SaveChanges();

                Assert.Equal(2, context2.ConnpassEventDataEntities.Count());

                testMock.Dispose();
            }
        }
    }
}