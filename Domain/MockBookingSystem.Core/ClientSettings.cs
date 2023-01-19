namespace MockBookingSystem.Core
{
    public sealed record ClientSettings
    {
        public string JwtTokenKey { get; init; }
        public string TripxAPI { get; set; }
    }
}