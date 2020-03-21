using System;
using System.Collections.Generic;
using System.Linq;
using TraPa.DataAccess.EFCore.Impl.Classes;
using TraPa.DataAccess.EFCore.Public.Interfaces;
using TraPa.DataAccess.Repositories.Impl.Classes;
using TraPa.DataAccess.Repositories.Public.Interfaces;
using TraPa.Entities.Impl.Classes;
using TraPa.Entities.Public.Interfaces;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("TraPaConsole is starting.");
            
            //todo connection string
            IDataAccessor dataAccessor = new DataAccessor(false, "Data Source=ConsoleRunDatabase.db");
            using (var dbContext = dataAccessor.GetNewDatabaseContext())
            {
                //dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                //dbContext.Database.Migrate();
            }
            IDatabaseRepositoryFactory databaseRepositoryFactory = new DatabaseRepositoryFactory(dataAccessor);
            
            ITravelerRepository travelerRepository = databaseRepositoryFactory.GetNewTravelerRepository();
            ITravelDateRepository travelDateRepository = databaseRepositoryFactory.GetNewTravelDateRepository();
            ITravelerTravelDateReferenceRepository travelerTravelDateReferenceRepository =
                databaseRepositoryFactory.GetNewTravelerTravelDateReferenceRepository();

            Console.WriteLine("Provide the action you would like to execute \n" +
                              "add : add a new traveler to the database \n" +
                              "list : list all travelers \n" +
                              "delete : remove traveler with a given index \n" +
                              "addtravel : add a new travel to the database \n" +
                              "listtravel : list all travels \n" +
                              "deletetravel : remove travel with a given index \n" +
                              "addreference : add a new travel travel date reference to the database \n" +
                              "listreference : list all travel travel date references \n" +
                              "deletereference : remove travel travel date references with a given index \n" +
                              "q : exit");

            var flag = true;
            do
            {
                var input = Console.ReadLine();
                var punctuation = input.Where(Char.IsPunctuation).Distinct().ToArray();
                var words = input.Split().Select(x => x.Trim(punctuation)).ToList();

                switch (words[0])
                {
                    case "add":
                        try
                        {
                            var traveler = CreateTraveler(words);
                            traveler = travelerRepository.Add(traveler.FirstName, traveler.LastName, traveler.PhoneNumber, traveler.DirectionFrom, traveler.DirectionTo, traveler.DoesPay, traveler.Price);
                            Console.WriteLine($"Traveler was added. Id: '{traveler.Id}'");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Could not add the traveler to the database. Reason:");
                            Console.WriteLine(e);
                        }
                        break;
                    case "list":
                        var travelers = travelerRepository.GetAll();
                        WriteTravelers(travelers);
                        break;
                    case "delete":
                        try
                        {
                            var idToDelete = Int32.Parse(words[1]);
                            travelerRepository.Remove(idToDelete);
                            Console.WriteLine($"Traveler was removed. Id: {idToDelete}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Could not remove the traveler from the database. Reason:");
                            Console.WriteLine(e);
                        }
                        break;
                    case "addtravel":
                        try
                        {
                            var travel = CreateTravel(words);
                            travelDateRepository.Add(travel.TravelName, travel.DateOfTravel);
                            Console.WriteLine("Travel was added.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Could not add the travel to the database. Reason:");
                            Console.WriteLine(e);
                        }
                        break;
                    case "listtravel":
                        var travelDates = travelDateRepository.GetAll();
                        WriteTravels(travelDates);
                        break;
                    case "deletetravel":
                        try
                        {
                            var idToDelete = Int32.Parse(words[1]);
                            travelDateRepository.Remove(idToDelete);
                            Console.WriteLine($"Travel was removed. Id: {idToDelete}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Could not remove the travel from the database. Reason:");
                            Console.WriteLine(e);
                        }
                        break;
                    case "addreference":
                        try
                        {
                            var travelTravelDateReference = CreateTravelTravelDateReference(words);
                            travelerTravelDateReferenceRepository.Add(travelTravelDateReference.TravelerId, travelTravelDateReference.TravelDateId);
                            Console.WriteLine("Traveler - travel date reference was added.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Could not add the traveler - travel date reference to the database. Reason:");
                            Console.WriteLine(e);
                        }
                        break;
                    case "listreference":
                        var travelerTravelDateReferences = travelerTravelDateReferenceRepository.GetAll();
                        WriteTravelTravelDateReferences(travelerTravelDateReferences);
                        break;
                    case "deletereference":
                        try
                        {
                            var travelerId = Int32.Parse(words[1]);
                            var travelDateId = Int32.Parse(words[2]);
                            travelerTravelDateReferenceRepository.Remove(travelerId, travelDateId);
                            Console.WriteLine("Traveler - travel date reference was removed.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Could not remove the traveler - travel date reference from the database. Reason:");
                            Console.WriteLine(e);
                        }
                        break;
                    case "q":
                        flag = false;
                        Console.WriteLine("Stopping the application.");
                        break;
                    default:
                        Console.WriteLine(@"Invalid key: {0}, try again.", words[0]);
                        break;
                }

            } while (flag);
        }

        private static void WriteTravels(IList<ITravelDate> travelDates)
        {
            foreach (var travelDate in travelDates)
            {
                Console.WriteLine(
                    @"Id : {0} Travel Name: {1}, Date Of Travel: {2}", travelDate.Id, travelDate.TravelName,
                    travelDate.DateOfTravel);
            }
        }

        private static ITravelDate CreateTravel(List<string> words)
        {
            if (words.Count < 4)
            {
                throw new ArgumentException("Too few parameters provided to create travel.");
            }

            var traveler = new TravelDate()
            {
                Id = Int32.Parse(words[1]),
                TravelName = words[2],
                DateOfTravel = DateTime.Parse(words[3])
            };

            return traveler;
        }

        private static void WriteTravelers(IList<ITraveler> travelers)
        {
            foreach (var traveler in travelers)
            {
                Console.WriteLine(
                    @"Id : {0} First Name: {1}, Last Name: {2}, Phone Number: {3}, Direction From: {4}, Direction To: {5}, Does Pay: {6}, Price: {7}"
                    , traveler.Id, traveler.FirstName, traveler.LastName, traveler.PhoneNumber, traveler.DirectionFrom,
                    traveler.DirectionTo, traveler.DoesPay, traveler.Price);
            }
        }

        private static ITraveler CreateTraveler(IList<string> words)
        {
            if (words.Count < 9)
            {
                throw new ArgumentException("Too few parameters provided to create traveler.");
            }

            var traveler = new Traveler
            {
                Id = Int32.Parse(words[1]),
                FirstName = words[2],
                LastName = words[3],
                PhoneNumber = words[4],
                DirectionFrom = words[5],
                DirectionTo = words[6],
                DoesPay = bool.Parse(words[7]),
                Price = Int32.Parse(words[8])
            };

            return traveler;
        }

        private static void WriteTravelTravelDateReferences(IList<ITravelerTravelDateReference> travelerTravelDateReferences)
        {
            foreach (var travelDateReference in travelerTravelDateReferences)
            {
                Console.WriteLine(@"TravelerID : {0} TravelDateId: {1}", travelDateReference.TravelerId, travelDateReference.TravelDateId);
            }
        }

        private static ITravelerTravelDateReference CreateTravelTravelDateReference(List<string> words)
        {
            if (words.Count < 3)
            {
                throw new ArgumentException("Too few parameters provided to create traveler - travel date reference.");
            }

            var traveler = new TravelerTravelDateReference()
            {
                TravelerId = Int32.Parse(words[1]),
                TravelDateId = Int32.Parse(words[2]),
            };

            return traveler;
        }
    }
}