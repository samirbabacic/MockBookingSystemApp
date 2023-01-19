namespace MockBookingSystem.Entities
{
    public record User : BaseEntity
    {
        public string Username { get; init; }
        public byte[] PasswordHash { get; init; }
        public byte[] PasswordSalt { get; init; }
    }
}