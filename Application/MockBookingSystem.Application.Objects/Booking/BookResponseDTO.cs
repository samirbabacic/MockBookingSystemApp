using MockBookingSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.Objects.Booking
{
    public record BookResponseDTO
    {
        public string BookingCode { get; init; }
        public BookingStatus BookingStatus { get; init; }
    }
}
