using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.Objects.DestinationOption
{
    public class DestinationOptionDTO
    {
        public string OptionCode { get; set; }
        public int? HotelCode { get; init; }
        public int? FlightCode { get; init; }
        public string ArrivalAirport { get; init; }
        public double Price { get; init; }
    }
}
