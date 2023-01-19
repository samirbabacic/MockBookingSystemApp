using MockBookingSystem.Core.Enums;

namespace MockBookingSystemAPI.Responses.Booking
{
    public sealed record CheckStatusResponse
    {
        public BookingStatus Status { get; init; }
    }
}
