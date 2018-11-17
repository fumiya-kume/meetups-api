using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public class InmemoryDbTestMock<T> : IDisposable where T : DbContext
{
    private static readonly SqliteConnection Connection = new SqliteConnection("DataSource=:memory:");
    private readonly DbContextOptions _contextOptions = new DbContextOptionsBuilder<T>().UseSqlite(Connection).Options;

    public InmemoryDbTestMock()
    {
        Connection.Open();
        Context().Database.EnsureCreated();
    }

    public T Context() => (T) Activator.CreateInstance(typeof(T), _contextOptions);

    public void Dispose()
    {
        Context().Database.EnsureDeleted();
        Connection.Close();
    }
}