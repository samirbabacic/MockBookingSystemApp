using MockBookingSystem.Core.Enums;
using MockBookingSystem.Objects.Booking;

namespace MockBookingSystemAPI.Responses.Booking
{
    public sealed record BookResponse
    {
        public string BookingCode { get; init; }
        public BookingStatus BookingStatus { get; init; }
    }
}
