using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.Objects.DestinationOption
{
    public record DestinationAirportDTO
    {
        public int? FlightCode { get; init; }
        public string FlightNumber { get; init; }
        public string DepartureAirport { get; init; }
        public string ArrivalAirport { get; init; }
    }
}
