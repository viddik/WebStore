using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Dto.Users
{
    public class AddLoginDto
    {
        public Entities.User User { get; set; }
        public UserLoginInfo UserLoginInfo { get; set; }
    }
}