using System.Collections.Generic;
using System.Linq;
using TraPa.Entities.Public.Interfaces;
using TraPa.Models.Impl.Classes.DocumentGenerator;
using TraPa.Models.Public.Interfaces;
using TraPa.Models.Public.Interfaces.DocumentGenerator;

namespace TraPa.Models.Impl.Classes
{
    public class TravelWindowModel : ITravelWindowModel
    {
        private readonly IList<ITravelDate> _travelDates;
        private ITravelDate _selectedTravelDate;
        private readonly ITravelDocumentGenerator _travelDocumentGenerator;

        public TravelWindowModel(IList<ITravelDate> travelDates, ITravelDocumentGenerator travelDocumentGenerator)
        {
            _travelDates = travelDates;
            _travelDocumentGenerator = travelDocumentGenerator;
        }

        public void SelectTraveler(int id)
        {
            _selectedTravelDate = _travelDates.FirstOrDefault(x => x.Id == id);
        }

        public void DownloadTravelDocument()
        {
            _travelDocumentGenerator.GenerateDocument(null, null);
        }
    }
}
