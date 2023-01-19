using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.Objects.Destination
{
    public record SearchModel
    {
        [Required]
        public string Destination { get; init; }

        public string DepartureAirport { get; init; }

        public DateTime FromDate { get; init; }

        [Required]
        public DateTime ToDate { get; init; }
    }
}
