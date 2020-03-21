namespace TraPa.DataAccess.Repositories.Public.Interfaces
{
    public interface IDatabaseRepositoryFactory
    {
        ITravelerRepository GetNewTravelerRepository();
        ITravelDateRepository GetNewTravelDateRepository();
        ITravelerTravelDateReferenceRepository GetNewTravelerTravelDateReferenceRepository();
    }
}