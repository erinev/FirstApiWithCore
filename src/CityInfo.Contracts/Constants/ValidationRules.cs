namespace CityInfo.Contracts.Constants
{
    public static class ValidationRules
    {
        public static class PlaceToVisit
        {
            public static int MinimumNameLength = 1;
            public static int MaximumNameLength => 200;

            public static int MaximumDescriptionLength => 1000;

            public static int MininumAddressLength => 6;
            public static int MaximumAddressLength => 200;
        }
    }
}
