using System.ComponentModel.DataAnnotations;

namespace CityInfo.Contracts.Dtos
{
    public class PlaceToVisit
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
