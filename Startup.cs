using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using my_book_store_v1.Data;
using my_book_store_v1.Data.Services;
using my_book_store_v1.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1
{
    public class Startup
    {
        public string ConnectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = configuration.GetConnectionString("DefaultConnectionString");
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

           

            services.AddCors(option =>
            {
                option.AddPolicy("AllowAll", builder =>               
                     builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                 );
            });
            services.ConfigureJWT(Configuration);
            //services.AddAuthentication();
            services.AddAuthorization();
           
            services.ConfigreIdentity();
           
            services.AddAutoMapper(typeof(Startup));
            services.AddResponseCaching();
            services.AddMemoryCache();

            services.ConfigureRateLimitingOptions();
            services.AddHttpContextAccessor();

            services.ConfigureHttpCacheHeaders();
            services.AddControllers( config=> {
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120 });
            });
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));
            services.AddTransient<BookServices>();
            services.AddTransient<AuthorService>();
            services.AddTransient<PublihserService>();
            services.AddTransient<LogsServices>();
            services.AddTransient<AuthManager>();
            services.ConfigureVersioning();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "my_book_store_v1", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "my_book_store_v1", Version = "v2" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            app.UseCors("AllowAll");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "my_book_store_v1 v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "my_book_store_v1 v2");
                }
           
                
                );
            }
           
            app.UseHttpsRedirection();
            app.UseHttpCacheHeaders();
            app.UseResponseCaching();
            app.UseIpRateLimiting();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.ConfigureExceptionHandler(loggerFactory);
            //app.ConfigureCustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //AppDbInitializer.Seed(app);
        }
    }
}
