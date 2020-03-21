using System.IO;
using TraPa.Entities.Public.Interfaces;
using TraPa.Models.Public.Interfaces.DocumentGenerator;

namespace TraPa.Models.Impl.Classes.DocumentGenerator
{
    public class TravelDocumentCsvGenerator : ITravelDocumentGenerator
    {
        private readonly ITravelDocumentLanguageFactory _languageFactory;

        public TravelDocumentCsvGenerator(ITravelDocumentLanguageFactory languageFactory)
        {
            _languageFactory = languageFactory;
        }

        public void GenerateDocument(ITravelDate selectedTravelDate, string path)
        {
            using var streamWriter = new StreamWriter(path);
            var firstLine = $"Utazás, {selectedTravelDate.TravelName}";
            var secondLine = $"Dátum, {selectedTravelDate.DateOfTravel.ToShortDateString()}";
            var headerLine = $"Sorszám,Név,Telefon,Indulás,Érkezés,Fizet,Összeg";
            streamWriter.WriteLine(firstLine);
            streamWriter.WriteLine(secondLine);
            streamWriter.WriteLine(headerLine);
            var lineIndex = 1;
            foreach (var travelDateReference in selectedTravelDate.TravelerTravelDateReferences)
            {
                var traveler = travelDateReference.Traveler;
                var line = $"{lineIndex},{traveler.LastName} {traveler.FirstName},{traveler.PhoneNumber},{traveler.DirectionFrom},{traveler.DirectionTo}";
                if (traveler.DoesPay)
                {
                    line = $"{line},Igen,{traveler.Price}";
                }
                else
                {
                    line = $"{line},Nem,";
                }
                streamWriter.WriteLine(line);
                lineIndex++;
            }
            streamWriter.Flush();
        }
    }
}