using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TraPa.DataAccess.EFCore.Public.Interfaces;
using TraPa.DataAccess.Repositories.Public.Interfaces;
using TraPa.Entities.Impl.Classes;
using TraPa.Entities.Public.Interfaces;

namespace TraPa.DataAccess.Repositories.Impl.Classes
{
    public class TravelerTravelDateReferenceRepository : ITravelerTravelDateReferenceRepository
    {
        private readonly IDataAccessor _dataAccessor;

        public TravelerTravelDateReferenceRepository(IDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public ITravelerTravelDateReference Add(int travelerId, int travelDateId)
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();

            var travelReference = new TravelerTravelDateReference
            {
                TravelerId = travelerId,
                TravelDateId = travelDateId,
            };
            EntityEntry<TravelerTravelDateReference> entityEntry = dbContext.TravelerTravelDateReferences.Add(travelReference);
            dbContext.SaveChanges();

            return entityEntry.Entity;
        }

        public void Remove(int travelerId, int travelDateId)
        {
            var travelDate = Get(travelerId, travelDateId);
            Remove(travelDate);
        }

        public void Remove(ITravelerTravelDateReference travelerTravelDateReference)
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            dbContext.TravelerTravelDateReferences.Remove((TravelerTravelDateReference) travelerTravelDateReference);
            dbContext.SaveChanges();
        }

        public ITravelerTravelDateReference Get(int travelerId, int travelDateId)
        {
            return InnerGet(travelerId, travelDateId);
        }

        public ITravelerTravelDateReference InnerGet(int travelerId, int travelDateId)
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            var isAny = dbContext.TravelerTravelDateReferences.Any(x => x.TravelerId == travelerId && x.TravelDateId == travelDateId);
            if (isAny == false)
            {
                throw new InvalidDataException($"No TravelerTravelDateReferences found with given IDs: { travelerId} {travelDateId}");
            }
            var result = dbContext.TravelerTravelDateReferences.FirstOrDefault(x => x.TravelerId == travelerId && x.TravelDateId == travelDateId);
            return result;
        }

        public IList<ITravelerTravelDateReference> GetAll()
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            var matches = dbContext.TravelerTravelDateReferences.ToList();
            
            return matches.Cast<ITravelerTravelDateReference>().ToList();
        }
    }
}