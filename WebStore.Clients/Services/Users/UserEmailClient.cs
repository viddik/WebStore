using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public class UserEmailClient : UserStoreBaseClient, IUserEmailStore<User>
    {
        public UserEmailClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/useremail";
        }

        protected override string ServiceAddress { get; set; }

        #region IUserEmailStore
        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            var url = $"{ServiceAddress}/setEmail/{email}";
            return PostAsync(url, user);
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getEmail";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getEmailConfirmed";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            var url = $"{ServiceAddress}/setEmailConfirmed/{confirmed}";
            return PostAsync(url, user);
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/findByEmail/{normalizedEmail}";
            return GetAsync<User>(url);
        }

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getNormalizedEmail";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            var url = $"{ServiceAddress}/setEmail/{normalizedEmail}";
            return PostAsync(url, user);
        }
        #endregion
    }
}
