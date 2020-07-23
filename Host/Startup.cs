using Domain.Interfaces;
using Domain.Middleware;
using Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Domain
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
            var isTransient = Configuration.GetValue<bool>("IsGuidServiceTransient");
            var isScoped = Configuration.GetValue<bool>("IsGuidServiceScoped");
            var isSingleton = Configuration.GetValue<bool>("IsGuidServiceSingleton");

            if (isTransient)
                services.AddTransient<IGuidService, GuidService>();
            else if (isScoped)
                services.AddScoped<IGuidService, GuidService>();
            else if (isSingleton)
                services.AddSingleton<IGuidService, GuidService>();
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Register the guid middleware.
            app.UseMiddleware<GuidMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}