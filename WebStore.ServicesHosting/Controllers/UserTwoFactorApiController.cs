using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/usertwofactor")]
    public class UserTwoFactorApiController : Controller
    {
        private readonly UserStore<User> _userStore;

        public UserTwoFactorApiController(WebStoreContext context)
        {
            _userStore = new UserStore<User>(context)
            {
                AutoSaveChanges = true
            };
        }

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
    }
}