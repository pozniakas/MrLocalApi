using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MrLocal_API.Controllers.Exceptions;
using MrLocal_API.Controllers.LoggerService;
using MrLocal_API.Controllers.LoggerService.Interfaces;
using MrLocal_API.Repositories;
using MrLocal_API.Repositories.Helpers;
using MrLocal_API.Repositories.Interfaces;
using MrLocal_API.Services;
using MrLocal_API.Services.Interfaces;
using NLog;
using System;
using System.IO;

namespace MrLocal_API
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
            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();

            //services.AddScoped(typeof(Lazy<IEnumConverter>),typeof(EnumConverter));
            services.AddScoped<IEnumConverter, EnumConverter>();
            services.AddScoped(provider => new Lazy<IEnumConverter>(provider.GetService<IEnumConverter>));

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

