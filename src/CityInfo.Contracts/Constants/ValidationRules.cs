using System.ComponentModel.DataAnnotations;

namespace CityInfo.Contracts.Constants
{
    public static class ValidationRules
    {
        public static class PlaceToVisit
        {
            public static int MinimumNameLength = 1;
            public static int MaximumNameLength => 200;

            public static int MaximumDescriptionLength => 1000;

            public static int MiminumAddressLength => 10;
            public static int MaximumAddressLength => 200;
        }
    }
}
