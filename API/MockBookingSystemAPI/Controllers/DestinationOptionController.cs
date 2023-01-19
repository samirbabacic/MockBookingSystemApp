using Microsoft.AspNetCore.Mvc;
using MockBookingSystem.ServiceLayer.Contracts;
using MockBookingSystemAPI.Requests.DestinationOption;
using MockBookingSystemAPI.Responses.DestinationOption;

namespace MockBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationOptionController : ControllerBase
    {
        private readonly IDestinationOptionService _destinationOptionService;

        public DestinationOptionController(IDestinationOptionService destinationOptionService)
        {
            _destinationOptionService = destinationOptionService;
        }

        [HttpPost("Search")]
        public async Task<SearchResponse> Search(SearchRequest request)
        {
            var destinations = await _destinationOptionService.SearchAsync(request);

            return new SearchResponse { Options= destinations };
        }
    }
}
