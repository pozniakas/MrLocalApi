using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MrLocalApi.Controllers;
using MrLocalApi.Controllers.Exceptions;
using MrLocalApi.Controllers.LoggerService;
using MrLocalApi.Controllers.LoggerService.Interfaces;
using MrLocalBackend.Repositories;
using MrLocalBackend.Repositories.Helpers;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalBackend.Services;
using MrLocalBackend.Services.Helpers;
using MrLocalBackend.Services.Interfaces;
using MrLocalDb;
using NLog;
using System;
using System.IO;

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

            services.AddScoped<IRequestEvent, RequestEvent>();

            services.AddScoped<IEnumConverter, EnumConverter>();
            services.AddScoped(provider => new Lazy<IEnumConverter>(provider.GetService<IEnumConverter>));

            services.AddScoped<IValidateData, ValidateData>();
            services.AddScoped(provider => new Lazy<IValidateData>(provider.GetService<IValidateData>));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<ISearchService, SearchService>();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

