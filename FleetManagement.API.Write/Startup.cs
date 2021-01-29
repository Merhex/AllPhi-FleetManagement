using FleetManagement.BLL;
using FleetManagement.DAL;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FleetManagement.API.Write
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
            services
                .AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(typeof(BusinessLogicLayer).Assembly))
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);

            services.AddDbContext<FleetManagementContext>();
            services.AddRepositories();

            services.AddBusinessLogicDependencies();

            services.AddMediatR(typeof(Startup).Assembly);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                await context.Response.WriteAsJsonAsync(new { error = exception.Message });
            }));

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
