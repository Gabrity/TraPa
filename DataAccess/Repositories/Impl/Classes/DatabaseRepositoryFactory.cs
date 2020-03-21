using TraPa.DataAccess.EFCore.Public.Interfaces;
using TraPa.DataAccess.Repositories.Public.Interfaces;

namespace TraPa.DataAccess.Repositories.Impl.Classes
{
    public class DatabaseRepositoryFactory : IDatabaseRepositoryFactory
    {
        private readonly IDataAccessor _dataAccessor;

        public DatabaseRepositoryFactory(IDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public ITravelerRepository GetNewTravelerRepository()
        {
            return new TravelerRepository(_dataAccessor);
        }

        public ITravelDateRepository GetNewTravelDateRepository()
        {
            return new TravelDateRepository(_dataAccessor);
        }

        public ITravelerTravelDateReferenceRepository GetNewTravelerTravelDateReferenceRepository()
        {
            return new TravelerTravelDateReferenceRepository(_dataAccessor);
        }
    }
}