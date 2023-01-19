using MockBookingSystem.Objects.DestinationOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.ServiceLayer.Wrappers
{
    public interface ITripxClient
    {
        public Task<List<DestinationHotelDTO>> GetHotelsAsync(string destinationCode);
        public Task<List<DestinationAirportDTO>> GetAirportsAsync(string departureAirport, string arrivalAirport);
    }
}
