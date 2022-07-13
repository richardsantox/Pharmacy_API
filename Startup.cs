using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pharmacy.src.context;
using Pharmacy.src.repositories;
using Pharmacy.src.repositories.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Pharmacy
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
            // Database Configuration
            if (Configuration["Enviroment:Start"] == "PROD")
            {
                services.AddEntityFrameworkNpgsql()
                     .AddDbContext<PharmacyContext>(
                     opt => opt.UseNpgsql(Configuration["ConnectionStringsPROD:DefaultConnection"]));
            }
            else
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<PharmacyContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            }

            // Controllers Configuration
            services.AddCors();
            services.AddControllers();

            // Repositories Configuration
            services.AddScoped<IPatient, PatientRepository>();
            services.AddScoped<IMedicine, MedicineRepository>();
            services.AddScoped<IMedicationControl, MedicationControlRepository>();

            // Swagger Configuratin 
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Pharmacy", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PharmacyContext context)
        {
            // Development environment
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pharmacy v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            // Production environment
            context.Database.EnsureCreated();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pharmacy v1");
                c.RoutePrefix = string.Empty;
            });

            // Routes
            app.UseRouting();

            app.UseCors(c => c
                  .AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
              );

            // Authorization
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
