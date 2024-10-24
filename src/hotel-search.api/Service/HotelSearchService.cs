namespace hotel_search.api
{
    public class HotelSearchService
    {
        private readonly IHotelSearchRepository _repository;

        public HotelSearchService(IHotelSearchRepository repository)
        {
            _repository = repository;
        }

        public List<Hotel> GetHotels(Location location)
        {
            return _repository
                .GetAll()
                .OrderBy(hotel =>
                {
                    return LocationDistance.CalculateMeters(
                        location.Latitude, location.Longitude,
                        double.Parse(hotel.LocationLatitude), double.Parse(hotel.LocationLongitude));
                })
                .ThenBy(hotel => hotel.Price)
                .ToList();
        }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public static class LocationDistance
    {
        public static double CalculateKilometers(double lat1, double lon1, double lat2, double lon2)
        {
            // https://stackoverflow.com/questions/6366408/calculating-distance-between-two-latitude-and-longitude-geocoordinates
            double rad(double angle) => angle * 0.017453292519943295769236907684886127d; // = angle * Math.Pi / 180.0d
            double havf(double diff) => Math.Pow(Math.Sin(rad(diff) / 2d), 2); // = sin²(diff / 2)
            return 12745.6 * Math.Asin(Math.Sqrt(havf(lat2 - lat1) + Math.Cos(rad(lat1)) * Math.Cos(rad(lat2)) * havf(lon2 - lon1))); // earth radius 6.372,8‬km x 2 = 12745.6
        }

        public static double CalculateMeters(double lat1, double lon1, double lat2, double lon2)
        {
            return CalculateKilometers(lat1, lon1, lat2, lon2) / 1000;
        }
    }
}
