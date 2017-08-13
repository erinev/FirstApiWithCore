using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.Contracts.Readmodel
{
    public class CityDocument
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
        public List<PlaceToVisitDocument> PlacesToVisit { get; set; } = new List<PlaceToVisitDocument>();
    }
}
