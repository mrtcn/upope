﻿using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Upope.Identity.DbContext;
using Upope.Identity.Entities;
using Upope.Identity.Filters;
using Upope.Identity.GlobalSettings;
using Upope.Identity.Helpers;
using Upope.Identity.Helpers.Interfaces;
using Upope.Identity.Models.FacebookResponse;
using Upope.Identity.Models.GoogleResponse;
using Upope.Identity.Services;
using Upope.Identity.Services.Interfaces;
using Upope.Identity.Services.Sync;
using Upope.Loyalty.Services;
using Upope.Loyalty.Services.Interfaces;
using Upope.ServiceBase.Handler;

namespace Upope.Identity
{
    public class Startup
    {
        private List<CultureInfo> supportedCultures
        {
            get
            {
                return new List<CultureInfo>
                {
                    new CultureInfo("tr-TR"),
                    new CultureInfo("en-US"),
                };
            }
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            BuildAppSettingsProvider();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Add Entity Framework and Identity Framework

            services.AddDbContext<ApplicationUserDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationUserDbContext>();

            services.AddTransient<IRandomPasswordHelper, RandomPasswordHelper>();

            services.AddTransient<IExternalAuthClient, FacebookClient>();
            services.AddTransient<IExternalAuthClient, GoogleClient>();

            services.AddTransient<IExternalAuthService<FacebookResponse>, FacebookService>(sp => {
                var externalAuthService = sp.GetRequiredService<IEnumerable<IExternalAuthClient>>();
                return new FacebookService(externalAuthService.FirstOrDefault());
            });

            services.AddTransient<IExternalAuthService<GoogleResponse>, GoogleService>(sp => {
                var externalAuthService = sp.GetRequiredService< IEnumerable<IExternalAuthClient>>();
                return new GoogleService(externalAuthService.Skip(1).Take(1).FirstOrDefault());
            });

            #endregion

            #region Add Authentication
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddFacebook(options =>
            {
                options.AppId = Configuration["ExternalAuthentication:Facebook:AppId"];
                options.AppSecret = Configuration["ExternalAuthentication:Facebook:AppSecret"];
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["ExternalAuthentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["ExternalAuthentication:Google:ClientSecret"];
            }) 
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Tokens:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            #endregion

            #region Localization
            services.AddLocalization(l =>
            {
                l.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    var userLangs = context.Request.Headers["Accept-Language"].ToString();
                    var firstLang = userLangs.Split(',').FirstOrDefault();
                    var defaultLang = string.IsNullOrEmpty(firstLang) ? supportedCultures.FirstOrDefault().TwoLetterISOLanguageName : firstLang;
                    return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang));
                }));
            });
            #endregion
            services.AddMvc(options => {
                options.Filters.Add<GlobalExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Upope Identity API", Version = "v1" });
            });

            services.AddAutoMapper();

            services.AddHttpClient();
            services.AddTransient<IHttpHandler, HttpHandler>();
            services.AddTransient<IChallengeUserSyncService, ChallengeUserSyncService>();
            services.AddTransient<ILoyaltySyncService, LoyaltySyncService>();
            services.AddTransient<ILoyaltyService, LoyaltyService>();
            services.AddTransient<IGameUserSyncService, GameUserSyncService>();
            services.AddTransient<IImageService, ImageService>();
            
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            //services.AddOcelot(Configuration);
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

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles(); // For the wwwroot folder

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                                    Path.Combine(Directory.GetCurrentDirectory(), @"UserImage")),
                RequestPath = new PathString("/UserImage")
            });
            //Localization
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("tr-TR"),
                new CultureInfo("en-US"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(supportedCultures.FirstOrDefault().ToString()),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Upope Identity API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
            //await app.UseOcelot();
        }

        private void BuildAppSettingsProvider()
        {
            AppSettingsProvider.GatewayUrl = Configuration["GatewayUrl"].ToString();
            AppSettingsProvider.ChallengeBaseUrl = Configuration["Upope.Challenge:BaseUrl"].ToString();
            AppSettingsProvider.ChallengeCreateOrUpdateUser = Configuration["Upope.Challenge:CreateOrUpdate"].ToString();

            AppSettingsProvider.LoyaltyBaseUrl = Configuration["Upope.Loyalty:BaseUrl"].ToString();
            AppSettingsProvider.CreateOrUpdateLoyalty = Configuration["Upope.Loyalty:CreateOrUpdate"].ToString();
            AppSettingsProvider.LoyaltyCreateOrUpdateUser = Configuration["Upope.Loyalty:UserCreateOrUpdate"].ToString();
            AppSettingsProvider.LoyaltyUserStats = Configuration["Upope.Loyalty:UserStats"].ToString();

            AppSettingsProvider.GameBaseUrl = Configuration["Upope.Game:BaseUrl"].ToString();
            AppSettingsProvider.CreateOrUpdateGameUser = Configuration["Upope.Game:CreateOrUpdateGameUser"].ToString();
        }
    }
}
