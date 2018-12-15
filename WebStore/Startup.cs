using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using WebStore.Clients;
using WebStore.Clients.Services.Employees;
using WebStore.Clients.Services.Orders;
using WebStore.Clients.Services.Products;
using WebStore.Clients.Services.Users;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Clients;
using WebStore.Interfaces.Services;
using WebStore.Logger;
using WebStore.Services;
using WebStore.Services.Middleware;

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
            services.AddTransient<IValuesService, ValuesClient>();

            // Подключение БД
            //services.AddDbContext<WebStoreContext>(options => options.UseSqlServer(
            //    Configuration.GetConnectionString("DefaultConnection")));

            // Подключение Microsoft.AspNetCore.Identity
            //services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<WebStoreContext>()
            //    .AddDefaultTokenProviders();

            // Настройка Identity
            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders();

            services.AddTransient<IUserStoreClient, UserStoreClient>();

            // ПРОБЛЕМА:
            // Не получается заменить UsersClient на UserStoreClient,
            // замена вызывает падение SignInManager<User> _signInManager
            // в WebStore.Controllers.AccountController,
            // в методе public async Task<IActionResult> Login(LoginViewModel model)
            services.AddTransient<IUserStore<User>, UsersClient>();

            services.AddTransient<IUserRoleStore<User>, UserRoleClient>();
            services.AddTransient<IUserClaimStore<User>, UserClaimClient>();
            services.AddTransient<IUserPasswordStore<User>, UserPasswordClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UserTwoFactorClient>();
            services.AddTransient<IUserEmailStore<User>, UserEmailClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UserPhoneNumberClient>();
            services.AddTransient<IUserLoginStore<User>, UserLoginClient>();
            services.AddTransient<IUserLockoutStore<User>, UserLockoutClient>();
            services.AddTransient<IRoleStore<IdentityRole>, RolesClient>();

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
                options.Cookie.Expiration = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to / Account / Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to / Account / Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to / Account / AccessDenied
                options.SlidingExpiration = true;
            });

            // Настройки для корзины
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICartService, CookieCartService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Включаем логирование Log4Net
            loggerFactory.AddLog4Net();

            // Добавляем middleware для обработки исключений
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            // Включаем расширение для использования статических файлов,
            // т.к. appsettings.json - статический файл
            app.UseStaticFiles();

            // Добавляем аутентификацию
            app.UseAuthentication();

            // Назначаем URL для отображение ошибочных статусов HTTP
            app.UseStatusCodePagesWithRedirects("~/home/errorstatus/{0}");

            // Добавляем middleware для логирования исключений
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

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
