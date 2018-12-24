using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/useremail")]
    public class UserEmailApiController : Controller
    {
        private readonly UserStore<User> _userStore;

        public UserEmailApiController(WebStoreContext context)
        {
            _userStore = new UserStore<User>(context)
            {
                AutoSaveChanges = true
            };
        }

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
    }
}