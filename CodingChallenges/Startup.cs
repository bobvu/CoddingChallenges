using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CodingChallenges.DataAcess.DbContexts;
using Microsoft.EntityFrameworkCore;
using CodingChallenges.Domains.Users;
using Microsoft.AspNetCore.Identity;
using CodingChallenges.Helpers;
using CodingChallenges.DataAcess.Repos;
using CodingChallenges.Services;
using AutoMapper;
using CodingChallenges.ViewModels;

namespace CodingChallenges
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Add framework services.
            services.AddDbContext<SqlContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CodingChallenges")));

            services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<SqlContext>()
                .AddDefaultTokenProviders();
            // Add cors
            services.AddCors();

            // Add framework services.
            services.AddMvc();


            // DB Creation and Seeding
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

            //Register Repository
            services.AddTransient(typeof(IRepo<>), typeof(SqlRepo<>));

            services.AddTransient(typeof(IEmployeeService), typeof(EmployeeService));

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseOptions();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Warning);
            loggerFactory.AddFile(Configuration.GetSection("Logging"));
            Utilities.ConfigureLogger(loggerFactory);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
