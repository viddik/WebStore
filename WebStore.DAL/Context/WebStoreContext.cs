using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;

namespace WebStore.DAL.Context
{
    /// <summary>
    /// Наследование от IdentityDbContext<User> вместо обычного DbContext
    /// требуется для подключения Microsoft.AspNetCore.Identity
    /// </summary>
    public class WebStoreContext : IdentityDbContext<User>
    {
        public WebStoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
