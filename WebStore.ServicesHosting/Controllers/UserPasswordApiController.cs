using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Dto.Users;
using WebStore.Domain.Entities;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/userpassword")]
    public class UserPasswordApiController : Controller
    {
        private readonly UserStore<User> _userStore;

        public UserPasswordApiController(WebStoreContext context)
        {
            _userStore = new UserStore<User>(context) { AutoSaveChanges = true };
        }

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
    }
}