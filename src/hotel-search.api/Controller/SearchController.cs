using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace hotel_search.api
{
    [Route("api/search")]
    [ApiController]
    //[Authorize]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly HotelSearchService _hotelSearchService;

        public SearchController(ILogger<SearchController> logger, HotelSearchService searchService)
        {
            _logger = logger;
            _hotelSearchService = searchService;
        }

        [HttpGet("{latitude}/{longitude}/{page?}/{pageSize?}")]
        public IActionResult Search(string latitude, string longitude, int page = 0, int pageSize = 100)
        {
            try
            {
                latitude = HttpUtility.UrlDecode(latitude);
                longitude = HttpUtility.UrlDecode(longitude);
                _logger.LogInformation($"Searching for hotels around location: {latitude},{longitude}");
                var location = new Location { Latitude = double.Parse(latitude), Longitude = double.Parse(longitude) };

                return Ok(_hotelSearchService.GetHotels(location, page, pageSize));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
