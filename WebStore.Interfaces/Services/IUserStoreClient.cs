using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IUserStoreClient : IUserStore<User>
    {
    }
}