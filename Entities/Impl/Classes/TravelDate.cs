using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TraPa.Entities.Public.Interfaces;

namespace TraPa.Entities.Impl.Classes
{
    public class TravelDate : ITravelDate
    {
        public int Id { get; set; }
        [Display(Name = "Travel Name")]
        public string TravelName { get; set; }
        [Display(Name = "Date of Travel")]
        public DateTime DateOfTravel { get; set; }
        public List<TravelerTravelDateReference> TravelerTravelDateReferences { get; set; }

        IReadOnlyList<ITravelerTravelDateReference> ITravelDate.TravelerTravelDateReferences => TravelerTravelDateReferences ?? new List<TravelerTravelDateReference>();
    }
}