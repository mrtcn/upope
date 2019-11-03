using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Upope.Challenge.Services.Interfaces;
using Upope.Challenge.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Upope.Challenge.GlobalSettings;
using Upope.ServiceBase.Handler;
using Upope.Challenge.Hubs;
using Swashbuckle.AspNetCore.Swagger;
using Upope.Game.Services.Interfaces;
using Upope.Challenge.Services.Sync;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Linq;
using Upope.Challenge.Filters;
using Upope.ServiceBase.Services.Interfaces;
using Upope.ServiceBase.Services;

namespace Upope.Challenge
{
    public class Startup
    {
        private List<CultureInfo> supportedCultures { get {
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
            #region Add Authentication
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;                

            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = true,
                    ValidAudience = this.Configuration["Tokens:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = this.Configuration["Tokens:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
                config.Validate(JwtBearerDefaults.AuthenticationScheme);
                // We have to hook the OnMessageReceived event in order to
                // allow the JWT authentication handler to read the access
                // token from the query string when a WebSocket or 
                // Server-Sent Events request comes in.
                config.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/challengehub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
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
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ApplicationDbContext>(opt => {
                opt.UseSqlServer(Configuration["ConnectionStrings:UpopeChallenge"]);
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Upope Challenge API", Version = "v1" });
            });

            services.AddAutoMapper();

            services.AddHttpClient();
            services.AddTransient<IHttpHandler, HttpHandler>();

            services.AddTransient<IChallengeService, ChallengeService>();
            services.AddTransient<IBotService, BotService>();
            services.AddTransient<IChallengeRequestService, ChallengeRequestService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGameSyncService, GameSyncService>();
            services.AddTransient<IGeoLocationService, GeoLocationService>();

            services.AddSignalR(hubOptions => {
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
            //app.UseHttpsRedirection();
            app.UseAuthentication();

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Upope Challenge API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();

            app.UseSignalR(routes => routes.MapHub<ChallengeHub>("/challengehub"));
        }

        private void BuildAppSettingsProvider()
        {
            AppSettingsProvider.IdentityBaseUrl = Configuration["Upope.Identity:BaseUrl"].ToString();
            AppSettingsProvider.GetUserId = Configuration["Upope.Identity:GetUserId"].ToString();
            AppSettingsProvider.GetUserProfile = Configuration["Upope.Identity:GetUserProfile"].ToString();
            AppSettingsProvider.Login = Configuration["Upope.Identity:Login"].ToString();


            AppSettingsProvider.ChallengeBaseUrl = Configuration["Upope.Challenge:BaseUrl"].ToString();
            AppSettingsProvider.UpdateChallenge = Configuration["Upope.Challenge:UpdateChallenge"].ToString();

            
            AppSettingsProvider.LoyaltyBaseUrl = Configuration["Upope.Loyalty:BaseUrl"].ToString();      
            AppSettingsProvider.SufficientPointsUrl = Configuration["Upope.Loyalty:SufficientPointsUrl"].ToString();
            AppSettingsProvider.FilterUsersUrl = Configuration["Upope.Loyalty:FilterUsersUrl"].ToString();

            AppSettingsProvider.GameBaseUrl = Configuration["Upope.Game:BaseUrl"].ToString();
            AppSettingsProvider.CreateGameUrl = Configuration["Upope.Game:CreateGameUrl"].ToString();
        }
    }
}
