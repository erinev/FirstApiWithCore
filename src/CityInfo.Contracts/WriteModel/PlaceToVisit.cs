using System;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.Contracts.WriteModel
{
    [Serializable]
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
