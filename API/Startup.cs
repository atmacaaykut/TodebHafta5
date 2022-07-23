using Bussines.Abstract;
using Bussines.Concrete;
using Bussines.Configuration.Mapper;
using Bussines.Configuration.Validator.FluentValidation;
using DAL.Abstract;
using DAL.Concrete.EF;
using DAL.DbContexts;
using DTO.Customer;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //DI, Singleton, Scope, Transit

            //ICustomerSevice  -- aadeneme, CustomerService
            //ICustomerRepository -- EFCustomerRepository, DapperRepository

            services.AddDbContext<TodebCampDbContext>(ServiceLifetime.Transient);

            services.AddAutoMapper(config =>
            {
                config.AddProfile(new MapperProfile());
            });

            var companyName = Configuration.GetValue<string>("CompanyName");

            if (companyName == "KahveDunyasi")
                services.AddScoped<ICustomerService, KahveDunyasiCustomerService>();
            else
                services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<ICustomerRepository, EFCustomerRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
