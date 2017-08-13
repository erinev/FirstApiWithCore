using System.ComponentModel.DataAnnotations;

namespace CityInfo.Contracts.Dtos
{
    public class PointOfInterest
    {
        [Required]
        public int Id { get; set; }
    }
}
