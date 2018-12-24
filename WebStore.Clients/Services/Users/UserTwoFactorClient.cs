using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public class UserTwoFactorClient : UserStoreBaseClient, IUserTwoFactorStore<User>
    {
        public UserTwoFactorClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/usertwofactor";
        }

        protected sealed override string ServiceAddress { get; set; }

        #region IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            var url = $"{ServiceAddress}/setTwoFactor/{enabled}";
            return PostAsync(url, user);
        }

        public async Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getTwoFactorEnabled";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }
        #endregion
    }
}
