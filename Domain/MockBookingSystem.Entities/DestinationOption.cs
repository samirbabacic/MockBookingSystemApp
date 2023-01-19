using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.Entities
{
    public record DestinationOption : BaseEntity
    {
        public override string Id { get { return Code; } }

        private string _code;

        public string Code
        {
            get
            {
                if (!string.IsNullOrEmpty(_code))
                    return _code;

                if (FlightCode == null)
                {
                    _code = HotelCode.ToString();
                    return _code;
                }

                _code = $"{HotelCode}_{FlightCode}";

                return _code;
            }
            private set { _code = value; }
        }

        public int HotelCode { get; init; }
        public int? FlightCode { get; init; }
        public string DepartureAirport { get; init; }
        public string ArrivalAirport { get; init; }
        public double Price { get; init; }
    }
}
