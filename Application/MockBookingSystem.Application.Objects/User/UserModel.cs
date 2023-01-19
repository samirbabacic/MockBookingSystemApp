namespace MockBookingSystem.Application.Objects.User
{
    public record UserModel
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}