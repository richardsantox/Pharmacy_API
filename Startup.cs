using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pharmacy.src.context;
using Pharmacy.src.repositories;
using Pharmacy.src.repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PharmacyContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Controllers Configuration
            services.AddControllers();

            // Repositories Configuration
            services.AddScoped<IPatient, PatientRepository>();
            services.AddScoped<IMedicine, MediceRepository>();
            services.AddScoped<IMedicationControl, MedicationControlRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PharmacyContext context)
        {
            // Development environment
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }

            context.Database.EnsureCreated();
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
