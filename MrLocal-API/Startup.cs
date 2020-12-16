using Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Backend.Models;
using Backend.Repositories;
using Backend.Repositories.Helpers;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Backend.Services.Helpers;
using Backend.Services.Interfaces;
using MrLocal_API.Controllers;
using MrLocal_API.Controllers.Exceptions;
using MrLocal_API.Controllers.LoggerService;
using MrLocal_API.Controllers.LoggerService.Interfaces;
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

            services.AddDbContext<MrlocalDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();

            services.AddScoped<IRequestEvent, RequestEvent>();

            services.AddScoped<IEnumConverter, EnumConverter>();
            services.AddScoped(provider => new Lazy<IEnumConverter>(provider.GetService<IEnumConverter>));

            services.AddScoped(typeof(IXmlRepository<>), typeof(XmlRepository<>));
            services.AddScoped(provider => new Lazy<IXmlRepository<Product>>(provider.GetService<IXmlRepository<Product>>));
            services.AddScoped(provider => new Lazy<IXmlRepository<Shop>>(provider.GetService<IXmlRepository<Shop>>));

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

