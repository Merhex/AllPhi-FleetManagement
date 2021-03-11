// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FleetManagement.IdentityServer4
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var migrationAssemblyName = typeof(Startup).Assembly.GetName().Name;

            services
                .AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options => 
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("IdentityServerDatabase"), 
                        sql => sql.MigrationsAssembly(migrationAssemblyName));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("IdentityServerDatabase"),
                        sql => sql.MigrationsAssembly(migrationAssemblyName));
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}
