using Microsoft.AspNetCore.Mvc;
using MockBookingSystem.Core.Enums;
using MockBookingSystem.ServiceLayer.Contracts;
using MockBookingSystemAPI.Requests.Booking;
using MockBookingSystemAPI.Responses.Booking;

namespace MockBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("Book")]
        public async Task<BookResponse> Book(BookRequest request)
        {
            var bookResponse = await _bookingService.BookAsync(request.OptionCode, request.SearchModel);

            return new BookResponse { BookingCode = bookResponse.BookingCode, BookingStatus = bookResponse.BookingStatus };
        }

        //I created the check for status here, as to me it is a responsability of the booking controller.
        [HttpPost("CheckStatus")]
        public async Task<CheckStatusResponse> CheckStatus(CheckStatusRequest request)
        {
            var status = await _bookingService.CheckStatusAsync(request.BookingCode);

            return new CheckStatusResponse { Status = status };
        }
    }
}
