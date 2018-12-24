using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public class UserPhoneNumberClient : UserStoreBaseClient, IUserPhoneNumberStore<User>
    {
        public UserPhoneNumberClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/userphonenumber";
        }

        protected override string ServiceAddress { get; set; }

        #region IUserPhoneNumberStore
        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            var url = $"{ServiceAddress}/setPhoneNumber/{phoneNumber}";
            return PostAsync(url, user);
        }

        public async Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getPhoneNumber";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getPhoneNumberConfirmed";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            var url = $"{ServiceAddress}/setPhoneNumberConfirmed/{confirmed}";
            return PostAsync(url, user);
        }
        #endregion
    }
}
