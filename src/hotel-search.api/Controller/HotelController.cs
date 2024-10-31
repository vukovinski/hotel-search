using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        public ActionResult Post([FromBody] HotelDto hotel)
        {
            var hotelId = _hotels.GetNextId();
            var success = _hotels.InsertHotel(new Hotel
            {
                Id = hotelId,
                Name = hotel.Name,
                Price = hotel.Price,
                LocationLatitude = hotel.Latitude,
                LocationLongitude = hotel.Longitude
            });
            return success ? CreatedAtAction("Get", hotelId) : BadRequest();
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] HotelDto hotel)
        {
            var success = _hotels.UpdateHotel(new Hotel
            {
                Id = id,
                Name = hotel.Name,
                Price = hotel.Price,
                LocationLatitude = hotel.Latitude,
                LocationLongitude = hotel.Longitude
            });
            return success ? Ok() : BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return _hotels.DeleteHotel(new Hotel { Id = id }) ? Ok() : BadRequest();
        }
    }

    public class HotelDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
