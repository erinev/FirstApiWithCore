using System.ComponentModel.DataAnnotations;

namespace CityInfo.Contracts.WriteModel
{
    public class PlaceToVisit
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
