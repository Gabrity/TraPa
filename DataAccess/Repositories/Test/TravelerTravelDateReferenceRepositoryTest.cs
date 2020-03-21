using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraPa.DataAccess.EFCore.Impl.Classes;
using TraPa.DataAccess.EFCore.Public.Interfaces;
using TraPa.DataAccess.Repositories.Impl.Classes;
using TraPa.DataAccess.Repositories.Public.Interfaces;

namespace TraPa.DataAccess.Repositories.Impl.Test
{
    [TestClass]
    public class TravelerTravelDateReferenceRepositoryTest
    {
        [TestMethod]
        public void TravelerTravelDateReferenceRepositoryOperations()
        {
            IDataAccessor dataAccessor = new DataAccessor(false, "Data Source=TravelerTravelDateRepositoryTest.db");
            using (var dbContext = dataAccessor.GetNewDatabaseContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
            ITravelerTravelDateReferenceRepository travelerTravelDateReferenceRepository = new TravelerTravelDateReferenceRepository(dataAccessor);
            ITravelerRepository travelerRepository = new TravelerRepository(dataAccessor);
            ITravelDateRepository travelDateRepository = new TravelDateRepository(dataAccessor);

            travelerRepository.Add("Zoltan","Zoltan", "+3655555", "Ada", "Bregenz", true, 500);
            travelerRepository.Add("Zoltan", "Zoltan", "+3655555", "Ada", "Bregenz", true, 500);

            travelDateRepository.Add("Oda", new DateTime(2020, 11, 9));
            travelDateRepository.Add("Vissza", new DateTime(2020, 11, 9));

            travelerTravelDateReferenceRepository.Add(1, 1);
            travelerTravelDateReferenceRepository.Add(1, 2);
            travelerTravelDateReferenceRepository.Add(2, 1);
            travelerTravelDateReferenceRepository.Add(2, 2);
            var results1 = travelerTravelDateReferenceRepository.GetAll();
            Assert.IsTrue(results1.Count == 4);
            
            travelerTravelDateReferenceRepository.Remove(1, 1);
            var results2 = travelerTravelDateReferenceRepository.GetAll();
            Assert.IsTrue(results2.Count == 3);

            travelDateRepository.Remove(2);
            var results3 = travelerTravelDateReferenceRepository.GetAll();
            Assert.IsTrue(results3.Count == 1);

            travelerRepository.Remove(2);
            var results4 = travelerTravelDateReferenceRepository.GetAll();
            Assert.IsTrue(results4.Count == 0);
        }
    }
}