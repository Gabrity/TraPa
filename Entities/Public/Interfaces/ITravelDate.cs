using System;
using System.Collections.Generic;

namespace TraPa.Entities.Public.Interfaces
{
    public interface ITravelDate
    {
        int Id { get; set; }
        string TravelName { get; set; }
        DateTime DateOfTravel { get; set; }
        IReadOnlyList<ITravelerTravelDateReference> TravelerTravelDateReferences { get; }
    }
}