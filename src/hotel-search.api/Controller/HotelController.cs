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
        public void Post([FromBody] HotelDto hotel)
        {
            _hotels.InsertHotel(new Hotel
            {
                Id = _hotels.GetNextId(),
                Name = hotel.Name,
                Price = hotel.Price,
                LocationLatitude = hotel.Lat,
                LocationLongitude = hotel.Lon
            });
        }

        [HttpPost("{id}")]
        public void Update(int id, [FromBody] HotelDto hotel)
        {
            _hotels.UpdateHotel(new Hotel
            {
                Id = id,
                Name = hotel.Name,
                Price = hotel.Price,
                LocationLatitude = hotel.Lat,
                LocationLongitude = hotel.Lon
            });
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _hotels.DeleteHotel(new Hotel { Id = id });
        }
    }

    public class HotelDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
    }
}
