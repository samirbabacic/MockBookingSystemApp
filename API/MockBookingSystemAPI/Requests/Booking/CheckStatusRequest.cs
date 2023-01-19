using System.ComponentModel.DataAnnotations;

namespace MockBookingSystemAPI.Requests.Booking
{
    public sealed record CheckStatusRequest
    {
        [Required]
        public string BookingCode { get; init; }
    }
}
