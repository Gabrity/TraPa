using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraPa.DataAccess.EFCore.Impl.Classes;
using TraPa.DataAccess.EFCore.Public.Interfaces;
using TraPa.DataAccess.Repositories.Impl.Classes;
using TraPa.DataAccess.Repositories.Public.Interfaces;

namespace TraPa.DataAccess.Repositories.Impl.Test
{
    [TestClass]
    public class TravelerRepositoryTest
    {
        [TestMethod]
        public void TravelerRepositoryOperations()
        {
            IDataAccessor dataAccessor = new DataAccessor(false, "Data Source=TravelerRepositoryOperationTest.db");
            using (var dbContext = dataAccessor.GetNewDatabaseContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
            ITravelerRepository travelerRepository = new TravelerRepository(dataAccessor);

            travelerRepository.Add("Zoltan", "Zoltan", "+3655555", "Ada", "Bregenz", true, 500);
            travelerRepository.Add("Zoltan", "Zoltan", "+3655555", "Ada", "Bregenz", true, 500);
            var result = travelerRepository.Get(1);
            var results = travelerRepository.GetAll();

            Assert.IsTrue(results.Count == 2);
            Assert.IsTrue(result.FirstName == "Zoltan");

            travelerRepository.Remove(1);
            travelerRepository.Remove(2);
            results = travelerRepository.GetAll();
            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void TravelerRepositoryUpdate()
        {
            IDataAccessor dataAccessor = new DataAccessor(false, "Data Source=TravelerRepositoryUpdateTest.db");
            using (var dbContext = dataAccessor.GetNewDatabaseContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
            ITravelerRepository travelerRepository = new TravelerRepository(dataAccessor);

            travelerRepository.Add("Gabor", "Gabor", "+3655555", "Ada", "Bregenz", true, 500);
            travelerRepository.Add("Zoltan", "Zoltan", "+3655555", "Ada", "Bregenz", true, 500);
            
            var allTravelers = travelerRepository.GetAll();
            var traveler2 = allTravelers[1];

            Assert.IsTrue(allTravelers.Count == 2);
            Assert.IsTrue(traveler2.FirstName == "Zoltan");

            traveler2.FirstName = "Ian";
            travelerRepository.Update(traveler2);

            var modifiedAllTravelers = travelerRepository.GetAll();
            var modifiedTraveler2 = modifiedAllTravelers[1];

            Assert.IsTrue(modifiedTraveler2.FirstName == "Ian");
            Assert.IsTrue(allTravelers.Count == 2);
        }
    }
}