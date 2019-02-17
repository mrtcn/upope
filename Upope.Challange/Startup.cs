﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Upope.ServiceBase;
using Upope.Challange.Data.Entities;
using Upope.Challange.Services.Interfaces;
using Upope.Challange.Services;

namespace Upope.Challange
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ApplicationDbContext>(opt => {
                opt.UseSqlServer(Configuration["ConnectionStrings:UpopeChallenge"]);
            });
            services.AddAutoMapper(conf => {
                //conf.CreateMap<IEnt>
            });
            services.AddTransient<IChallengeService, ChallengeService>(sp => {
                var applicationDb = sp.GetRequiredService<ApplicationDbContext>();
                return new ChallengeService(applicationDb);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
