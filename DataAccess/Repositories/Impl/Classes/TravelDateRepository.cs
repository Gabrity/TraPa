using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraPa.DataAccess.EFCore.Public.Interfaces;
using TraPa.DataAccess.Repositories.Public.Interfaces;
using TraPa.Entities.Impl.Classes;
using TraPa.Entities.Public.Interfaces;

namespace TraPa.DataAccess.Repositories.Impl.Classes
{
    public class TravelDateRepository : ITravelDateRepository
    {
        private readonly IDataAccessor _dataAccessor;

        public TravelDateRepository(IDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public ITravelDate Add(string travelName, DateTime dateOfTravel)
        {
            
            var travelDate = new TravelDate()
            {
                TravelName = travelName,
                DateOfTravel = dateOfTravel
            };

            var entityEntry = InnerAdd(travelDate);

            return entityEntry;
        }

        public ITravelDate InnerAdd(ITravelDate travelDate)
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            var travelDateEntity = dbContext.TravelDates.Add((TravelDate) travelDate);
            dbContext.SaveChanges();

            return travelDateEntity.Entity;
        }

        public void Update(ITravelDate travelDate)
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            dbContext.Update(travelDate);
            dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var travelDate = Get(id);
            Remove(travelDate);
        }

        public void Remove(ITravelDate travelDate)
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            dbContext.TravelDates.Remove((TravelDate) travelDate);
            dbContext.SaveChanges();
        }

        public ITravelDate Get(int id)
        {
            return InnerGet(id);
        }

        public TravelDate InnerGet(int id)
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            var isAny = dbContext.TravelDates.Any(x => x.Id == id);
            if (isAny == false)
            {
                throw new InvalidDataException($"No TravelDate found with given ID : {id}");
            }
            var result = dbContext.TravelDates.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public IList<ITravelDate> GetAll()
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            var matches = dbContext.TravelDates.ToList();

            return matches.Cast<ITravelDate>().ToList();
        }
    }
}