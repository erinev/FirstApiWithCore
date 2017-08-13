using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.Contracts.Dtos
{
    public class City
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int NumberOfPlacesToVisit => PlacesToVisit.Count;

        [Required]
        public List<PlaceToVisit> PlacesToVisit { get; set; } = new List<PlaceToVisit>();
    }
}
