using System;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.Contracts.Responses
{
    [Serializable]
    public class PlaceToVisitDocument
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
