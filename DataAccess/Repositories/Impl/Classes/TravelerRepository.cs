using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraPa.DataAccess.EFCore.Public.Interfaces;
using TraPa.DataAccess.Repositories.Public.Interfaces;
using TraPa.Entities.Impl.Classes;
using TraPa.Entities.Public.Interfaces;

namespace TraPa.DataAccess.Repositories.Impl.Classes
{
    public class TravelerRepository : ITravelerRepository
    {
        private readonly IDataAccessor _dataAccessor;

        public TravelerRepository(IDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public ITraveler Add(string firstName, string lastName, string phoneNumber, string directionFrom, string directionTo, bool doesPay, int price)
        {
            Traveler traveler = new Traveler
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                DirectionFrom = directionFrom,
                DirectionTo = directionTo,
                DoesPay = doesPay,
                Price = price
            };

            return InnerAdd(traveler);
        }

        public ITraveler InnerAdd(ITraveler traveler)
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            var travelerEntity = dbContext.Travelers.Add((Traveler) traveler);
            dbContext.SaveChanges();

            return travelerEntity.Entity;
        }

        public void Update(ITraveler traveler)
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            dbContext.Update(traveler);
            dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var traveler = InnerGet(id);
            Remove(traveler);
        }

        public void Remove(ITraveler traveler)
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            dbContext.Travelers.Remove((Traveler) traveler);
            dbContext.SaveChanges();
        }

        public ITraveler Get(int id)
        {
            return InnerGet(id);
        }

        internal Traveler InnerGet(int id)
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            var isAny = dbContext.Travelers.Any(x => x.Id == id);
            if (isAny == false)
            {
                throw new InvalidDataException($"No Traveler found with given ID : {id}");
            }
            var result = dbContext.Travelers.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public IList<ITraveler> GetAll()
        {
            using var dbContext = _dataAccessor.GetNewDatabaseContext();
            var matches = dbContext.Travelers.Cast<ITraveler>().ToList();

            return matches;
        }
    }
}