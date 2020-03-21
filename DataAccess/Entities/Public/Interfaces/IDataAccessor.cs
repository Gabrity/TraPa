namespace TraPa.DataAccess.EFCore.Public.Interfaces
{
    public interface IDataAccessor
    {
        IDatabaseContext GetNewDatabaseContext();
    }
}