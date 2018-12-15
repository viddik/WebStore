using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/userrole")]
    public class UserRoleApiController : Controller
    {
        private readonly UserStore<User> _userStore;

        public UserRoleApiController(WebStoreContext context)
        {
            _userStore = new UserStore<User>(context) { AutoSaveChanges = true };
        }

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
    }
}