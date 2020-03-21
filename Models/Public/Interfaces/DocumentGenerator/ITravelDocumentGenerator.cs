using TraPa.Entities.Public.Interfaces;

namespace TraPa.Models.Public.Interfaces.DocumentGenerator
{
    public interface ITravelDocumentGenerator
    {
        void GenerateDocument(ITravelDate selectedTravelDate, string path);
    }
}