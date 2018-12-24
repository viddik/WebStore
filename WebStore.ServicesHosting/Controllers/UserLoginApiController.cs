using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Dto.Users;
using WebStore.Domain.Entities;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/userlogin")]
    public class UserLoginApiController : Controller
    {
        private readonly UserStore<User> _userStore;

        public UserLoginApiController(WebStoreContext context)
        {
            _userStore = new UserStore<User>(context)
            {
                AutoSaveChanges = true
            };
        }

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
    }
}