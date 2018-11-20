using System.Globalization;
using System.Linq;
using meetupsApi.Domain.Entity;
using meetupsApi.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace meetupsApi.Tests.Helper
{
    public class InmemoryDbTestMockTests
    {
        [Fact]
        void DBにデータを保存することができる()
        {
            using (var testMock = new InmemoryDbTestMock<DummyDbContext>())
            {
                var context = testMock.Context();

                var person = new person
                {
                    Name = "Kuxu",
                    Age = 10
                };
                context.Persons.Add(person);
                context.SaveChanges();

                Assert.Equal(1, context.Persons.Count());
                testMock.Dispose();
            }
        }

        [Fact]
        void DBに連続して書き込みを行ってもDisposableな実装にしていると以前のデータベースは消えている()
        {
            using (var testMock = new InmemoryDbTestMock<DummyDbContext>())
            {
                var context = testMock.Context();

                var person = new person
                {
                    Name = "Kuxu",
                    Age = 10
                };
                context.Persons.Add(person);
                context.SaveChanges();

                Assert.Equal(1, context.Persons.Count());
                testMock.Dispose();
            }

            using (var testMock = new InmemoryDbTestMock<DummyDbContext>())
            {
                var context = testMock.Context();

                Assert.Equal(0, context.Persons.Count());

                var person = new person
                {
                    Name = "Kuxu",
                    Age = 10
                };
                context.Persons.Add(person);
                context.SaveChanges();

                Assert.Equal(1, context.Persons.Count());
                testMock.Dispose();
            }
        }
    }

    public class person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class DummyDbContext : DbContext
    {
        public DummyDbContext(DbContextOptions<DummyDbContext> options) : base(options)
        {
        }

        public DbSet<person> Persons { get; set; }
    }
}