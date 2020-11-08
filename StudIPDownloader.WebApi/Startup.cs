using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudIPDownloader.WebApi.Controllers;
using StudIPDownloader.WebApi.HostedServices;

namespace StudIPDownloader.WebApi
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
            services.AddControllers();
            services.AddSwaggerDocument();
            services.AddTransient<StudipDownloadController>();
            services.AddTransient<ILoginActivityService, LoginActivityServiceWithSelenium>();
            services.AddSingleton<InputContract>(x =>
            {
                return new InputContract()
                {
                    Url = Environment.GetEnvironmentVariable("STUDIP_URL"),
                    Password = Environment.GetEnvironmentVariable("STUDIP_PASSWORD"),
                    Username = Environment.GetEnvironmentVariable("STUDIP_USERNAME"),
                    TimeSpan = TimeSpan.FromMinutes(int.Parse(Environment.GetEnvironmentVariable("TIMER"))),
                    
                };
            });
            services.AddHostedService<TimedHostedService>(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            // app.UseHttpsRedirection();
            app.UseSwaggerUi3();
            app.UseOpenApi();
            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}