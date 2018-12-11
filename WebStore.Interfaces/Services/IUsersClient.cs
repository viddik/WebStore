﻿using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IUsersClient : 
        //IUserRoleStore<User>,
        //IUserClaimStore<User>,
        IUserPasswordStore<User>,
        IUserTwoFactorStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserLoginStore<User>,
        IUserLockoutStore<User>
    {
    }
}
