namespace hotel_search.api
{
    public class HotelSearchService
    {
        private readonly IHotelSearchRepository _repository;

        public HotelSearchService(IHotelSearchRepository repository)
        {
            _repository = repository;
        }

        public List<HotelWithDistance> GetHotels(Location location, int page = 0, int pageSize = 100)
        {
            return _repository
                .GetAll()
                .Select(hotel =>
                {
                    return new HotelWithDistance
                    {
                        Id = hotel.Id,
                        Name = hotel.Name,
                        Price = hotel.Price,
                        LocationLatitude = hotel.LocationLatitude,
                        LocationLongitude = hotel.LocationLongitude,
                        Distance = LocationDistance.CalculateKilometers(
                            location.Latitude, location.Longitude,
                            double.Parse(hotel.LocationLatitude), double.Parse(hotel.LocationLongitude))
                    };
                })
                .OrderBy(hotelWithDistance => hotelWithDistance.Distance)
                .ThenBy(hotel => hotel.Price)
                .Skip(page * pageSize).Take(pageSize)
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
            return 12745.6 * Math.Asin(Math.Sqrt(havf(lat2 - lat1) + Math.Cos(rad(lat1)) * Math.Cos(rad(lat2)) * havf(lon2 - lon1))); // earth radius 6.372,8 km x 2 = 12745.6
        }

        public static double CalculateMeters(double lat1, double lon1, double lat2, double lon2)
        {
            return CalculateKilometers(lat1, lon1, lat2, lon2) * 1000;
        }
    }
}
