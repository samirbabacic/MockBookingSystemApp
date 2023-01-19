using MockBookingSystem.Objects.Destination;
using System.ComponentModel.DataAnnotations;

namespace MockBookingSystemAPI.Requests.Booking
{
    public record BookRequest
    {
        [Required]
        public string OptionCode { get; set; }

        [Required]
        public SearchModel SearchModel { get; set; }
    }
}
