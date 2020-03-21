namespace TraPa.Entities.Public.Interfaces
{
    public interface ITravelerTravelDateReference
    {
        int TravelerId { get; set; }
        ITraveler Traveler { get; }

        int TravelDateId { get; set; }
        ITravelDate TravelDate { get; }
    }
}