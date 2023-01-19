using MockBookingSystem.Core.Enums;
using MockBookingSystem.Objects.Booking;
using MockBookingSystem.Objects.Destination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.ServiceLayer.Contracts
{
    public interface IBookingService
    {
        Task<BookResponseDTO> BookAsync(string optionCode, SearchModel searchModel);
        Task<BookingStatus> CheckStatusAsync(string bookingCode);
    }
}
