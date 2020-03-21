using TraPa.Entities.Public.Interfaces;

namespace TraPa.Entities.Impl.Classes
{
    public class TravelerTravelDateReference : ITravelerTravelDateReference
    {
        public int TravelerId { get; set; }
        public Traveler Traveler { get; set; }

        public int TravelDateId { get; set; }
        public TravelDate TravelDate { get; set; }
        ITraveler ITravelerTravelDateReference.Traveler => Traveler;
        ITravelDate ITravelerTravelDateReference.TravelDate => TravelDate;
    }
}