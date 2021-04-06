using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QuestionsApp.Web.DB;
using QuestionsApp.Web.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsApp.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuration for Entity Framework
            services.AddDbContext<QuestionsContext>(options => options.UseInMemoryDatabase("Dummy"));
            // Registration and configuration of the MVC Framework
            services.AddControllers();
            // Configuration for the Swagger Generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Questions API", Version = "v1" });
            });
            // Configuration for SignalR
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            // Activate swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Questions API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                // Activate MVC Controllers for WebApi
                endpoints.MapControllers();

                // Activate SignalR Hub
                endpoints.MapHub<QuestionsHub>("/hub");

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Error");
                });
            });
        }
    }
}
