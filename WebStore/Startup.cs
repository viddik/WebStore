using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebStore.Clients;
using WebStore.Clients.Services.Employees;
using WebStore.Clients.Services.Orders;
using WebStore.Clients.Services.Products;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Clients;
using WebStore.Interfaces.Services;
using WebStore.Services;
using WebStore.Services.InMemory;
using WebStore.Services.Sql;

namespace WebStore
{
    public class Startup
    {
        /// <summary>
        /// Свойство для доступа к конфигурации.
        /// Экземпляр, реализующий данный интерфейс, 
        /// автоматически при старте считывает все секции файла конфигурации
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Конструктор, принимающий интерфейс IConfiguration
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Добавляем​​ сервисы,​​ необходимые​​ для​​ mvc
            services.AddMvc();

            // Внедрение зависимостей
            // Singleton-объекты создаются один раз при запуске приложения, и при всех запросах к приложению оно использует один и тот же singleton-объект
            //services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            // Scoped-объекты создаются по одному на каждый запрос
            //services.AddScoped<IProductData, SqlProductData>();
            // Transient-объекты создаются каждый раз, когда нам требуется экземпляр определенного класса
            // Подробнее см https://metanit.com/sharp/aspnet5/6.5.php
            services.AddTransient<IEmployeesData, EmployeesClient>();
            services.AddTransient<IProductData, ProductsClient>();
            services.AddTransient<IOrdersService, OrdersClient>();

            // Подключение БД
            services.AddDbContext<WebStoreContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            // Подключение Microsoft.AspNetCore.Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<WebStoreContext>()
                .AddDefaultTokenProviders();

            // Конфигурация Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 6;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
                // User settings
                //options.User.RequireUniqueEmail = true;
            });

            // Конфигурация Cookie
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromHours(1);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to / Account / Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to / Account / Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to / Account / AccessDenied
                options.SlidingExpiration = true;
            });

            // Настройки для корзины
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CookieCartService>();

            // Добавляем реализацию клиента
            services.AddTransient<IValuesService, ValuesClient>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Включаем расширение для использования статических файлов,
            // т.к. appsettings.json - статический файл
            app.UseStaticFiles();

            var hello = Configuration["CustomHelloWorld"];

            // Добавляем аутентификацию
            app.UseAuthentication();

            // Добавляем​​ обработку​​ запросов​​ в​​ mvc-формате
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "areas",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
