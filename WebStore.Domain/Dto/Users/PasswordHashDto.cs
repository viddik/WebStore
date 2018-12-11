namespace WebStore.Domain.Dto.Users
{
    public class PasswordHashDto
    {
        public Entities.User User { get; set; }
        public string Hash { get; set; }
    }
}