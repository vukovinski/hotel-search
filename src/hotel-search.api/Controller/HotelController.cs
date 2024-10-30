using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_search.api
{
    [Route("api/hotels")]
    [ApiController]
    //[Authorize]
    public class HotelController : ControllerBase
    {
        private readonly IHotelSearchRepository _hotels;

        public HotelController(IHotelSearchRepository hotels)
        {
            _hotels = hotels;
        }

        [HttpGet]
        public IEnumerable<Hotel> Get()
        {
            return _hotels.GetAll();
        }

        [HttpGet("{id}")]
        public Hotel Get(int id)
        {
            return _hotels.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody] CreateHotel hotel)
        {
            _hotels.InsertHotel(new Hotel
            {
                Id = _hotels.GetNextId(),
                Name = hotel.Name,
                Price = hotel.Price,
                LocationLatitude = hotel.Latitude,
                LocationLongitude = hotel.Longitude
            });
        }

        [HttpPut]
        public void Update([FromBody] Hotel hotel)
        {
            _hotels.UpdateHotel(hotel);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _hotels.DeleteHotel(new Hotel { Id = id });
        }
    }

    public class CreateHotel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
