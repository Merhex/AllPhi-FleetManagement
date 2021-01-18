using AutoMapper;
using FleetManagement.API.Read.Mappings.Profiles;
using FleetManagement.BLL.Components;
using FleetManagement.BLL.Components.Interfaces;
using FleetManagement.BLL.Validators;
using FleetManagement.BLL.Validators.Interfaces;
using FleetManagement.DAL;
using FleetManagement.DAL.Repositories;
using FleetManagement.DAL.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace FleetManagement.Read
{
    public class Startup
    {
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureDevelopmentServices(IServiceCollection services) 
        {
            services.AddDbContext<FleetManagementContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("FleetManagementDatabase"));
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FleetManagement.API.Read", Version = "v1" });
            });


            ConfigureServices(services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddMediatR(typeof(Startup));

            services.AddDbContext<FleetManagementContext>();

            services.AddTransient<IDriverComponent, DriverComponent>();
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<IBelgianNationalNumberValidator, BelgianNationalNumberValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
