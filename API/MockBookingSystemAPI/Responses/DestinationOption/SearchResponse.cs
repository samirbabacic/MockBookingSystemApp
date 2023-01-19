using MockBookingSystem.Objects.DestinationOption;

namespace MockBookingSystemAPI.Responses.DestinationOption
{
    public sealed record SearchResponse
    {
        public List<DestinationOptionDTO> Options { get; set; } = new();
    }
}
