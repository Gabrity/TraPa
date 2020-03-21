using System.Collections.Generic;

namespace TraPa.Entities.Public.Interfaces
{
    public interface ITraveler
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string DirectionFrom { get; set; }
        string DirectionTo { get; set; }
        bool DoesPay { get; set; }
        int Price { get; set; }
        IReadOnlyList<ITravelerTravelDateReference> TravelerTravelDateReferences { get; }
    }
}