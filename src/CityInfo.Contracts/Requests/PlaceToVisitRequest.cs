using System;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.Contracts.Requests
{
    [Serializable]
    public class PlaceToVisitRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
