using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.Core.Enums
{
    public enum BookingStatus
    {
        Pending,
        Failed,
        Success
    }

    public enum BookingType
    {
        HotelOnly,
        HotelAndFlight,
        LastMinuteHotel
    }
}
