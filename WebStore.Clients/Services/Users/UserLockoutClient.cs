using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain.Dto.Users;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public class UserLockoutClient : UserStoreBaseClient, IUserLockoutStore<User>
    {
        public UserLockoutClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/userlockout";
        }

        protected override string ServiceAddress { get; set; }

        #region IUserLockoutStore
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getLockoutEndDate";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<DateTimeOffset?>();
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEnd = lockoutEnd;
            var url = $"{ServiceAddress}/setLockoutEndDate";
            return PostAsync(url, new SetLockoutDto()
            {
                User = user,
                LockoutEnd = lockoutEnd
            });
        }

        public async Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/IncrementAccessFailedCount";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<int>();
        }

        public Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/ResetAccessFailedCount";
            return PostAsync(url, user);
        }

        public async Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/GetAccessFailedCount";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<int>();
        }

        public async Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/GetLockoutEnabled";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        public async Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
            var url = $"{ServiceAddress}/SetLockoutEnabled/{enabled}";
            await PostAsync(url, user);
        }
        #endregion
    }
}
