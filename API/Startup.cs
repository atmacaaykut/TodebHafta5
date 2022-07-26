﻿using BackgroundJobs.Abstract;
using BackgroundJobs.Concrete;
using BackgroundJobs.Concrete.HangfireJobs;
using Bussines.Abstract;
using Bussines.Concrete;
using Bussines.Configuration.Mapper;
using DAL.Abstract;
using DAL.Concrete.EF;
using DAL.DbContexts;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using API.Configuration.Filters.Exception;
using API.Configuration.Filters.Logs;
using Cache;
using Cache.Redis;
using DAL.Concrete.Mongo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using ServiceStack.Redis;
using StackExchange.Redis;
using RedisEndpointInfo = Bussines.Configuration.Cache.RedisEndpointInfo;

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



            services.AddDbContext<TodebCampDbContext>(ServiceLifetime.Transient);

            var redisConfigInfo = Configuration.GetSection("RedisEndpointInfo").Get<RedisEndpointInfo>();

            /*eğer kendi yazdığımız cachemanager kullanacak isek AddStackExchangeRedisCache kısmı 
            commentlenebilir. aşağıdaki singleton olan implementasyon RedisCacheManager içerisinde kullanılacak olan 
            RedisEndPoint içindir. RedisEndpoint bir kere tanımlanıp gelen her istekte aynı instance verilir
            
            services.AddSingleton<RedisEndpoint>(opt =>
            {
                return new RedisEndpoint
                {
                    Host = redisConfigInfo.EndPoint,
                    Port = redisConfigInfo.Port,
                    Username = redisConfigInfo.UserName,
                    Password = redisConfigInfo.Password,
                };
            });


             kendi yazmış olduğumuz CacheManager implemente ediyoruz. 
            AddStackExchangeRedisCache  kısmı commentlenebilir.


            services.AddScoped<ICacheManager, RedisCacheManager>();
            */


            //mongoDb
            services.AddSingleton<MongoClient>(x => new MongoClient("mongodb://localhost:27017"));
            services.AddScoped<ICrediCartRepository, CreditCardRepository>();
            services.AddScoped<ICreditCardService, CreditCardService>();
            services.AddSingleton<MsSqlLogger>();


            services.AddStackExchangeRedisCache(opt =>
            {
                opt.ConfigurationOptions = new ConfigurationOptions()
                {
                    EndPoints =
                    {
                        { redisConfigInfo.EndPoint, redisConfigInfo.Port }
                    },
                    Password = redisConfigInfo.Password,
                    User = redisConfigInfo.UserName

                };
            });
            services.AddMemoryCache();

           


            services.AddScoped<ICacheExample, CacheExample>();
            


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
            services.AddScoped<IJobs, HangfireJobs>();
            services.AddScoped<ISendMailService, SendMailService>();
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
   


            var tokenOptions = Configuration.GetSection("TokenOptions").Get<Bussines.Configuration.Auth.TokenOption>();
            services.AddAuthentication(opt=>
                {
                   opt.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
                   opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                   opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                    };
                });





            var hangFireDb = Configuration.GetConnectionString("HangfireConnection");

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(hangFireDb, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer();

            services.AddControllers(opt =>
            {
               // opt.Filters.Add<ExceptionFilter>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
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

            app.UseExceptionHandler(c=>c.Run(async context=>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>().Error;

                 var jsonResult = new JsonResult(
                    new
                    {
                        error = exception.Message,
                        innerException = exception.InnerException,
                        statusCode = HttpStatusCode.InternalServerError
                    }

                );
                 context.Response.StatusCode = (int)HttpStatusCode.BadRequest; 
                 await context.Response.WriteAsJsonAsync(jsonResult);
            }));




            app.UseHangfireDashboard("/TodebHangfire", new DashboardOptions()
            {
                
            });

            RecurringJob.AddOrUpdate<IJobs>(x=>x.ReccuringJob(), "0 15 * * *");


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
