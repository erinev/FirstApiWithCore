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

        public List<PointOfInterest> PointOfInterests { get; set; } = new List<PointOfInterest>();

        public int NumberOfPointOfInterests => PointOfInterests.Count;
    }
}
