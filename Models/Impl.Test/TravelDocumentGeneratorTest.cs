using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TraPa.Entities.Public.Interfaces;
using TraPa.Models.Impl.Classes.DocumentGenerator;

namespace TraPa.Models.Impl.Test
{
    [TestClass]
    public class TravelDocumentGeneratorTest
    {
        [TestMethod]
        public void GenerateDocument()
        {
            var travelDocumentCsvGenerator = new TravelDocumentCsvGenerator(new TravelDocumentLanguageFactoryHungarian());
            var mockTravelDate = new Mock<ITravelDate>();
            mockTravelDate.Setup(x => x.TravelName).Returns("[ExampleName]");
            mockTravelDate.Setup(x => x.DateOfTravel).Returns(new DateTime(2020, 9, 5));
            
            var reference1 = new Mock<ITravelerTravelDateReference>();
            var travelerMock1 = GenerateTravelerMock(5, "[FirstName1]", "[LastName1]", "[Phone1]", "[Start1]", "[End1]", false, 5);
            reference1.Setup(x => x.Traveler).Returns(travelerMock1.Object);

            var reference2 = new Mock<ITravelerTravelDateReference>();
            var travelerMock2 = GenerateTravelerMock(6, "[FirstName2]", "[LastName2]", "[Phone2]", "[Start2]", "[End2]", true, 5);
            reference2.Setup(x => x.Traveler).Returns(travelerMock2.Object);

            var travelReferenceList = new List<ITravelerTravelDateReference> {reference1.Object, reference2.Object};

            mockTravelDate.Setup(x => x.TravelerTravelDateReferences).Returns(travelReferenceList);
            travelDocumentCsvGenerator.GenerateDocument(mockTravelDate.Object, "TestCsvOutput.csv");
        }

        private Mock<ITraveler> GenerateTravelerMock(int id, string firstName, string lastName, string phone, string from, string to, bool doesPay, int price)
        {
            var traveler = new Mock<ITraveler>();
            traveler.Setup(x => x.Id).Returns(id);
            traveler.Setup(x => x.FirstName).Returns(firstName);
            traveler.Setup(x => x.LastName).Returns(lastName);
            traveler.Setup(x => x.PhoneNumber).Returns(phone);
            traveler.Setup(x => x.DirectionFrom).Returns(from);
            traveler.Setup(x => x.DirectionTo).Returns(to);
            traveler.Setup(x => x.DoesPay).Returns(doesPay);
            traveler.Setup(x => x.Price).Returns(price);

            return traveler;
        }
    }
}