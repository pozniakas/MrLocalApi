using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MrLocalApi.Controllers;
using MrLocalApi.Controllers.Exceptions;
using MrLocalApi.Controllers.LoggerService;
using MrLocalApi.Controllers.LoggerService.Interfaces;
using MrLocalBackend.Authentication;
using MrLocalBackend.Authentication.Interfaces;
using MrLocalBackend.Repositories;
using MrLocalBackend.Repositories.Helpers;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalBackend.Services;
using MrLocalBackend.Services.Helpers;
using MrLocalBackend.Services.Interfaces;
using MrLocalDb;
using NLog;
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace MrLocalApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/Configs/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<MrLocalDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<IRequestEvent, RequestEvent>();

            services.AddScoped<IEnumConverter, EnumConverter>();
            services.AddScoped(provider => new Lazy<IEnumConverter>(provider.GetService<IEnumConverter>));

            services.AddScoped<IValidateData, ValidateData>();
            services.AddScoped(provider => new Lazy<IValidateData>(provider.GetService<IValidateData>));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IJwdAuthenticationManager, JwdAuthenticationManager>();

            var key = ConfigurationManager.AppSettings.Get("SECRET_KEY");

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

