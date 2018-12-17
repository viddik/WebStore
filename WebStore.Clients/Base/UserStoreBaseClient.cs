using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Base
{
    public class UserStoreBaseClient : BaseClient, IUserStoreClient
    {
        protected override string ServiceAddress { get; set; }

        public UserStoreBaseClient(IConfiguration configuration) : base(configuration)
        {
        }

        public void Dispose()
        {
            Client.Dispose();
        }

        #region IUserStore
        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/userId";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/userName";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<string>();
            return ret;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            var url = $"{ServiceAddress}/userName/{userName}";
            return PostAsync(url, user);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/normalUserName";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            var url = $"{ServiceAddress}/normalUserName/{normalizedName}";
            return PostAsync(url, user);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }


        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user";
            var result = await PutAsync(url, user);
            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/delete";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/find/{userId}";
            return GetAsync<User>(url);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/normal/{normalizedUserName}";
            var result = await GetAsync<User>(url);
            return result;
        }

        #endregion

        //#region IUserStore
        //public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await UserStorage.GetUserIdAsync(user, cancellationToken);
        //}

        //public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await UserStorage.GetUserNameAsync(user, cancellationToken);
        //}

        //public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        //{
        //    return UserStorage.SetUserNameAsync(user, userName, cancellationToken);
        //}

        //public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await UserStorage.GetNormalizedUserNameAsync(user, cancellationToken);
        //}

        //public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        //{
        //    return UserStorage.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        //}

        //public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await UserStorage.CreateAsync(user, cancellationToken);
        //}


        //public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await UserStorage.UpdateAsync(user, cancellationToken);
        //}

        //public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        //{
        //    return await UserStorage.DeleteAsync(user, cancellationToken);
        //}

        //public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        //{
        //    return await UserStorage.FindByIdAsync(userId, cancellationToken);
        //}

        //public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        //{
        //    return await UserStorage.FindByNameAsync(normalizedUserName, cancellationToken);
        //}

        //#endregion
    }
}
