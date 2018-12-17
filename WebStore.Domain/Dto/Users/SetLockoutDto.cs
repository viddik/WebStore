using System;

namespace WebStore.Domain.Dto.Users
{
    public class SetLockoutDto
    {
        public Entities.User User { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}