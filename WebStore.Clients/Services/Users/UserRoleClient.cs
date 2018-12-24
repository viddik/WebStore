using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Services.Users
{
    public class UserRoleClient : UserStoreBaseClient, IUserRoleStore<User>
    {
        //private readonly IUserStoreClient _userStoreClient;

        public UserRoleClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/userrole";
        }

        protected sealed override string ServiceAddress { get; set; }

        //public void Dispose()
        //{
        //    Client.Dispose();
        //}

        //#region IUserStore
        //public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await _userStoreClient.GetUserIdAsync(user, cancellationToken);
        //}

        //public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await _userStoreClient.GetUserNameAsync(user, cancellationToken);
        //}

        //public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        //{
        //    return _userStoreClient.SetUserNameAsync(user, userName, cancellationToken);
        //}

        //public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await _userStoreClient.GetNormalizedUserNameAsync(user, cancellationToken);
        //}

        //public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        //{
        //    return _userStoreClient.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        //}

        //public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await _userStoreClient.CreateAsync(user, cancellationToken);
        //}


        //public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await _userStoreClient.UpdateAsync(user, cancellationToken);
        //}

        //public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await _userStoreClient.DeleteAsync(user, cancellationToken);
        //}

        //public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        //{
        //    return await _userStoreClient.FindByIdAsync(userId, cancellationToken);
        //}

        //public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        //{
        //    return await _userStoreClient.FindByNameAsync(normalizedUserName, cancellationToken);
        //}

        //#endregion

        #region IUserRoleStore

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/role/{roleName}";
            return PostAsync(url, user);
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/delete/{roleName}";
            return PostAsync(url, user);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/roles";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<IList<string>>();
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/inrole/{roleName}";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/usersInRole/{roleName}";
            IList<User> result = await GetAsync<List<User>>(url);
            return result.ToList();
        }

        #endregion
    }
}
