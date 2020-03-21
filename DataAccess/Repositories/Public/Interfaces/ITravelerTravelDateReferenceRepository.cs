using System.Collections.Generic;
using TraPa.Entities.Public.Interfaces;

namespace TraPa.DataAccess.Repositories.Public.Interfaces
{
    public interface ITravelerTravelDateReferenceRepository
    {
        ITravelerTravelDateReference Add(int travelerId, int travelDateId);
        void Remove(int travelerId, int travelDateId);
        void Remove(ITravelerTravelDateReference travelerTravelDateReference);
        ITravelerTravelDateReference Get(int travelerId, int travelDateId);
        IList<ITravelerTravelDateReference> GetAll();
    }
}