using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TraPa.Entities.Public.Interfaces;

namespace TraPa.Entities.Impl.Classes
{
    public class Traveler : ITraveler
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Direction From")]
        public string DirectionFrom { get; set; }
        [Display(Name = "Direction To")]
        public string DirectionTo { get; set; }
        [Display(Name = "Does Pay")]
        public bool DoesPay { get; set; }
        [Display(Name = "Price")]
        public int Price { get; set; }
        public List<TravelerTravelDateReference> TravelerTravelDateReferences { get; set; }

        IReadOnlyList<ITravelerTravelDateReference> ITraveler.TravelerTravelDateReferences => TravelerTravelDateReferences ?? new List<TravelerTravelDateReference>();
    }
}