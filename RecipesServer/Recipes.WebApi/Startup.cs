using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Recipes.Infrastructure;

namespace Recipes.WebApi
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
            services.ConfigureInfrastructureServices();
            services.ConfigureDatabase(Configuration.GetConnectionString("DefaultConnection"));

            services.AddCors();

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Recipes.WebApi", Version = "v1"});
                
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((document, _) =>
                    {
                        var paths = document.Paths.ToDictionary(item => item.Key.ToLowerInvariant(), item => item.Value);
                        document.Paths.Clear();
                        foreach (var pathItem in paths)
                        {
                            document.Paths.Add(pathItem.Key, pathItem.Value);
                        }
                    });
                });
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipes.WebApi v1"));
            }
            
            app.UseRouting();

            app.UseStaticFiles();
            
            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200"));
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}