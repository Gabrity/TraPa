using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraPa.DataAccess.EFCore.Impl.Classes;
using TraPa.DataAccess.EFCore.Public.Interfaces;
using TraPa.DataAccess.Repositories.Impl.Classes;
using TraPa.DataAccess.Repositories.Public.Interfaces;

namespace TraPa.DataAccess.Repositories.Impl.Test
{
    [TestClass]
    public class TravelDateRepositoryTest
    {
        [TestMethod]
        public void TravelDateRepositoryOperations()
        {
            IDataAccessor dataAccessor = new DataAccessor(false, "Data Source=TravelDateRepositoryTest.db");
            using (var dbContext = dataAccessor.GetNewDatabaseContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
            ITravelDateRepository travelDateRepository = new TravelDateRepository(dataAccessor);

            travelDateRepository.Add("Oda", new DateTime(2020, 11, 9));
            travelDateRepository.Add("Vissza", new DateTime(2020, 11, 9));
            var result = travelDateRepository.Get(1);
            var results = travelDateRepository.GetAll();

            Assert.IsTrue(results.Count == 2);
            Assert.IsTrue(result.TravelName == "Oda");
            Assert.IsTrue(result.TravelerTravelDateReferences.Count == 0);

            travelDateRepository.Remove(1);
            travelDateRepository.Remove(2);
            results = travelDateRepository.GetAll();
            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void TravelDateRepositoryUpdate()
        {
            IDataAccessor dataAccessor = new DataAccessor(false, "Data Source=TravelDateRepositoryUpdateTest.db");
            using (var dbContext = dataAccessor.GetNewDatabaseContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
            var travelDateRepository = new TravelDateRepository(dataAccessor);

            travelDateRepository.Add("First Travel", DateTime.MinValue);
            travelDateRepository.Add("Second Travel", DateTime.MaxValue);

            var allTravelDates = travelDateRepository.GetAll();
            var travelDate2 = allTravelDates[1];
            Assert.IsTrue(travelDate2.TravelerTravelDateReferences.Count == 0);

            Assert.IsTrue(allTravelDates.Count == 2);
            Assert.IsTrue(travelDate2.TravelName == "Second Travel");

            travelDate2.TravelName = "Modified Second Travel";
            travelDateRepository.Update(travelDate2);

            var modifiedAllTravelers = travelDateRepository.GetAll();
            var modifiedTraveler2 = modifiedAllTravelers[1];

            Assert.IsTrue(modifiedTraveler2.TravelName == "Modified Second Travel");
            Assert.IsTrue(allTravelDates.Count == 2);
        }
    }
}