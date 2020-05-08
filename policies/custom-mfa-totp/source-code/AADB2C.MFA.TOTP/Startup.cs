using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AADB2C.MFA.TOTP.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AADB2C.MFA.TOTP
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
            services.AddMvc();

            // Demo: Load the app settings section and bind to AppSettingsModel object graph
            services.Configure<AppSettingsModel>(Configuration.GetSection("AppSettings"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // app.UseCors("AllowAll");
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Requires the following import:
                    // using Microsoft.AspNetCore.Http;Access-Control-Allow-Origin
                    ctx.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    ctx.Context.Response.Headers.Add("Access-Control-Allow-Methods", "*");



                }
            });
            app.UseMvc();
        }
    }
}
