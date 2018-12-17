using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain.Dto.Users;
using WebStore.Domain.Entities;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly UserStore<User> _userStore;

        public UsersController(WebStoreContext context)
        {
            _userStore = new UserStore<User>(context)
            {
                AutoSaveChanges = true
            };
        }

        #region UserStore
        [HttpPost("userId")]
        public async Task<string> GetUserIdAsync([FromBody]User user)
        {
            return await _userStore.GetUserIdAsync(user);
        }

        [HttpPost("userName")]
        public async Task<string> GetUserNameAsync([FromBody]User user)
        {
            return await _userStore.GetUserNameAsync(user);
        }

        [HttpPost("userName/{userName}")]
        public async Task SetUserNameAsync([FromBody]User user, string userName)
        {
            await _userStore.SetUserNameAsync(user, userName);
        }

        [HttpPost("normalUserName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody]User user)
        {
            var result = await _userStore.GetNormalizedUserNameAsync(user);
            return result;
        }

        [HttpPost("normalUserName/{normalizedName}")]
        public async Task SetNormalizedUserNameAsync([FromBody]User user, string normalizedName)
        {
            await _userStore.SetNormalizedUserNameAsync(user, normalizedName);
        }

        [HttpPost("user")]
        public async Task<bool> CreateAsync([FromBody]User user)
        {
            var result = await _userStore.CreateAsync(user);
            return result.Succeeded;
        }

        [HttpPut("user")]
        public async Task<bool> UpdateAsync([FromBody]User user)
        {
            var result = await _userStore.UpdateAsync(user);
            return result.Succeeded;
        }

        [HttpPost("user/delete")]
        public async Task<bool> DeleteAsync([FromBody]User user)
        {
            var result = await _userStore.DeleteAsync(user);
            return result.Succeeded;
        }

        [HttpGet("user/find/{userId}")]
        public async Task<User> FindByIdAsync(string userId)
        {
            var result = await _userStore.FindByIdAsync(userId);
            return result;
        }

        [HttpGet("user/normal/{normalizedUserName}")]
        public async Task<User> FindByNameAsync(string normalizedUserName)
        {
            var result = await _userStore.FindByNameAsync(normalizedUserName);
            return result;
        }
        #endregion

        #region IUserRoleStore
        [HttpPost("role/{roleName}")]
        public async Task AddToRoleAsync([FromBody]User user, string roleName)
        {
            await _userStore.AddToRoleAsync(user, roleName);
        }

        [HttpPost("role/delete/{roleName}")]
        public async Task RemoveFromRoleAsync([FromBody]User user, string roleName)
        {
            await _userStore.RemoveFromRoleAsync(user, roleName);
        }

        [HttpPost("roles")]
        public async Task<IList<string>> GetRolesAsync([FromBody]User user)
        {
            return await _userStore.GetRolesAsync(user);
        }

        [HttpPost("inrole/{roleName}")]
        public async Task<bool> IsInRoleAsync([FromBody]User user, string roleName)
        {
            return await _userStore.IsInRoleAsync(user, roleName);
        }

        [HttpGet("usersInRole/{roleName}")]
        public async Task<IList<User>> GetUsersInRoleAsync(string roleName)
        {
            return await _userStore.GetUsersInRoleAsync(roleName);
        }
        #endregion

        #region IUserPasswordStore
        [HttpPost("setPasswordHash")]
        public async Task<string> SetPasswordHashAsync([FromBody]PasswordHashDto hashDto)
        {
            await _userStore.SetPasswordHashAsync(hashDto.User, hashDto.Hash);
            return hashDto.User.PasswordHash;
        }

        [HttpPost("getPasswordHash")]
        public async Task<string> GetPasswordHashAsync([FromBody]User user)
        {
            var result = await _userStore.GetPasswordHashAsync(user);
            return result;
        }

        [HttpPost("hasPassword")]
        public async Task<bool> HasPasswordAsync([FromBody]User user)
        {
            return await _userStore.HasPasswordAsync(user);
        }
        #endregion



        #region IUserTwoFactorStore
        [HttpPost("setTwoFactor/{enabled}")]
        public async Task SetTwoFactorEnabledAsync([FromBody]User user, bool enabled)
        {
            await _userStore.SetTwoFactorEnabledAsync(user, enabled);
        }

        [HttpPost("getTwoFactorEnabled")]
        public async Task<bool> GetTwoFactorEnabledAsync([FromBody]User user)
        {
            return await _userStore.GetTwoFactorEnabledAsync(user);
        }
        #endregion

        #region IUserEmailStore
        [HttpPost("setEmail/{email}")]
        public async Task SetEmailAsync([FromBody]User user, string email)
        {
            await _userStore.SetEmailAsync(user, email);
        }

        [HttpPost("getEmail")]
        public async Task<string> GetEmailAsync([FromBody]User user)
        {
            return await _userStore.GetEmailAsync(user);
        }

        [HttpPost("getEmailConfirmed")]
        public async Task<bool> GetEmailConfirmedAsync([FromBody]User user)
        {
            return await _userStore.GetEmailConfirmedAsync(user);
        }

        [HttpPost("setEmailConfirmed/{confirmed}")]
        public async Task SetEmailConfirmedAsync([FromBody]User user, bool confirmed)
        {
            await _userStore.SetEmailConfirmedAsync(user, confirmed);
        }

        [HttpGet("user/findByEmail/{normalizedEmail}")]
        public async Task<User> FindByEmailAsync(string normalizedEmail)
        {
            return await _userStore.FindByEmailAsync(normalizedEmail);
        }

        [HttpPost("getNormalizedEmail")]
        public async Task<string> GetNormalizedEmailAsync([FromBody]User user)
        {
            return await _userStore.GetNormalizedEmailAsync(user);
        }

        [HttpPost("setEmail/{normalizedEmail}")]
        public async Task SetNormalizedEmailAsync([FromBody]User user, string normalizedEmail)
        {
            await _userStore.SetNormalizedEmailAsync(user, normalizedEmail);
        }
        #endregion

        #region IUserPhoneNumberStore
        [HttpPost("setPhoneNumber/{phoneNumber}")]
        public async Task SetPhoneNumberAsync([FromBody]User user, string phoneNumber)
        {
            await _userStore.SetPhoneNumberAsync(user, phoneNumber);
        }

        [HttpPost("getPhoneNumber")]
        public async Task<string> GetPhoneNumberAsync([FromBody]User user)
        {
            return await _userStore.GetPhoneNumberAsync(user);
        }

        [HttpPost("getPhoneNumberConfirmed")]
        public async Task<bool> GetPhoneNumberConfirmedAsync([FromBody]User user)
        {
            return await _userStore.GetPhoneNumberConfirmedAsync(user);
        }

        [HttpPost("setPhoneNumberConfirmed/{confirmed}")]
        public async Task SetPhoneNumberConfirmedAsync([FromBody]User user, bool confirmed)
        {
            await _userStore.SetPhoneNumberConfirmedAsync(user, confirmed);
        }
        #endregion

        #region IUserLoginStore
        [HttpPost("addLogin")]
        public async Task AddLoginAsync([FromBody]AddLoginDto loginDto)
        {
            await _userStore.AddLoginAsync(loginDto.User,
            loginDto.UserLoginInfo);
        }

        [HttpPost("removeLogin/{loginProvider}/{providerKey}")]
        public async Task RemoveLoginAsync([FromBody]User user, string loginProvider, string providerKey)
        {
            await _userStore.RemoveLoginAsync(user, loginProvider, providerKey);
        }

        [HttpPost("getLogins")]
        public async Task<IList<UserLoginInfo>> GetLoginsAsync([FromBody]User user)
        {
            return await _userStore.GetLoginsAsync(user);
        }

        [HttpGet("user/findbylogin/{loginProvider}/{providerKey}")]
        public async Task<User> FindByLoginAsync(string loginProvider, string providerKey)
        {
            return await _userStore.FindByLoginAsync(loginProvider,
            providerKey);
        }
        #endregion

        #region IUserLockoutStore
        [HttpPost("getLockoutEndDate")]
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user)
        {
            return await _userStore.GetLockoutEndDateAsync(user);
        }

        [HttpPost("setLockoutEndDate")]
        public Task SetLockoutEndDateAsync(SetLockoutDto setLockoutDto)
        {
            return _userStore.SetLockoutEndDateAsync(setLockoutDto.User,
            setLockoutDto.LockoutEnd);
        }

        [HttpPost("IncrementAccessFailedCount")]
        public async Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return await _userStore.IncrementAccessFailedCountAsync(user);
        }

        [HttpPost("ResetAccessFailedCount")]
        public Task ResetAccessFailedCountAsync(User user)
        {
            return _userStore.ResetAccessFailedCountAsync(user);
        }

        [HttpPost("GetAccessFailedCount")]
        public async Task<int> GetAccessFailedCountAsync(User user)
        {
            return await _userStore.GetAccessFailedCountAsync(user);
        }

        [HttpPost("GetLockoutEnabled")]
        public async Task<bool> GetLockoutEnabledAsync(User user)
        {
            return await _userStore.GetLockoutEnabledAsync(user);
        }

        [HttpPost("SetLockoutEnabled/{enabled}")]
        public async Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            await _userStore.SetLockoutEnabledAsync(user, enabled);
            return;
        }
        #endregion
    }
}
