using AspNetCoreRateLimit;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using my_book_store_v1.Controllers.Versioning.V2;
using my_book_store_v1.Data;
using my_book_store_v1.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_book_store_v1.Exceptions
{
    public static class ServiceExtensions
    {

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(option =>
            {

                option.ReportApiVersions = true;
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
                //option.ApiVersionReader = new HeaderApiVersionReader("Custome-api-version");
                //option.ApiVersionReader = new MediaTypeApiVersionReader();

                option.Conventions.Controller<ReadersController>()
                .HasApiVersion(1, 0)
                .HasApiVersion(1, 4)
                .HasDeprecatedApiVersion(1, 8);
            });
        }
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            services.AddHttpCacheHeaders(
                (expirationOpt) =>
                {
                    expirationOpt.MaxAge = 80;
                    expirationOpt.CacheLocation = CacheLocation.Private;
                },
                (validationOpt) =>
                {
                    validationOpt.MustRevalidate = true;
                }
                    
                );
        }
        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
          {
              new RateLimitRule
              {
                  Endpoint ="*",
                  Limit =5,
                  Period = "20s"
              }

          };
            services.Configure<IpRateLimitOptions>(op =>
            {
               op.GeneralRules = rateLimitRules;
            });
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
        public static void ConfigreIdentity(this IServiceCollection services)
        {
            var Builder = services.AddIdentityCore<ApiUser>(x => x.User.RequireUniqueEmail = true);

            Builder = new IdentityBuilder(Builder.UserType, typeof(IdentityRole), services);

            Builder.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        }

        public static void ConfigureJWT(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtSetting = configuration.GetSection("Jwt");
            var key = Environment.GetEnvironmentVariable("KEY");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
              .AddJwtBearer(op =>
              {
                  op.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateLifetime = true,
                      ValidateAudience = false,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = jwtSetting.GetSection("Issure").Value,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))

                  };
              });
               
        }
    }
}
