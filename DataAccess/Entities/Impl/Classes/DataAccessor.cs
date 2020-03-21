using Microsoft.EntityFrameworkCore;
using TraPa.DataAccess.EFCore.Impl.Data;
using TraPa.DataAccess.EFCore.Public.Interfaces;

namespace TraPa.DataAccess.EFCore.Impl.Classes
{
    public class DataAccessor : IDataAccessor
    {
        private readonly bool _useSqlServer;
        private readonly string _connectionString;
        public DataAccessor(bool useSqlServer, string connectionString)
        {
            _useSqlServer = useSqlServer;
            _connectionString = connectionString;
        }

        public IDatabaseContext GetNewDatabaseContext()
        {
            DbContextOptionsBuilder<DbContext> optionsBuilder = new DbContextOptionsBuilder<DbContext>();

            if (_useSqlServer)
                optionsBuilder.UseSqlServer(_connectionString);
            else
                optionsBuilder.UseSqlite(_connectionString);

            DatabaseContext databaseContext = new DatabaseContext(optionsBuilder.Options);

            return databaseContext;
        }
    }
}