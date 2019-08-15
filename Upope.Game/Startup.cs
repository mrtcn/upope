using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Upope.Game.Filters;
using Upope.Game.GlobalSettings;
using Upope.Game.Hubs;
using Upope.Game.Interfaces;
using Upope.Game.Managers;
using Upope.Game.Services;
using Upope.Game.Services.Interfaces;
using Upope.Game.Services.Sync;
using Upope.ServiceBase.Handler;
using Upope.ServiceBase.Services;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Game
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
                            (path.StartsWithSegments("/gamehubs")))
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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Upope Game API", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(opt => {
                opt.UseSqlServer(Configuration["ConnectionStrings:UpopeGame"]);
            });
            services.AddAutoMapper();
            services.AddHttpClient();

            services.AddTransient<IHttpHandler, HttpHandler>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IRoundAnswerService, RoundAnswerService>();
            services.AddTransient<IGameRoundService, GameRoundService>();
            services.AddTransient<IGameManager, GameManager>();
            services.AddTransient<IPointService, PointService>();
            services.AddTransient<IBluffService, BluffService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILoyaltySyncService, LoyaltySyncService>();

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Upope Game API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();

            app.UseSignalR(routes => routes.MapHub<GameHubs>("/gamehubs"));
        }

        private void BuildAppSettingsProvider()
        {
            AppSettingsProvider.IdentityBaseUrl = Configuration["Upope.Identity:BaseUrl"].ToString();
            AppSettingsProvider.GetUserId = Configuration["Upope.Identity:GetUserId"].ToString();
            AppSettingsProvider.GetUserProfileUrl = Configuration["Upope.Identity:GetUserProfileUrl"].ToString();
            AppSettingsProvider.WinRoundCount = int.Parse(Configuration["WinRoundCount"].ToString());
            AppSettingsProvider.WinInARowModal = int.Parse(Configuration["WinInARowModal"].ToString());
            AppSettingsProvider.NotificationBaseUrl = Configuration["Upope.Notification:BaseUrl"].ToString();
            AppSettingsProvider.SendNotification = Configuration["Upope.Notification:SendNotification"].ToString();
            AppSettingsProvider.LoyaltyBaseUrl = Configuration["Upope.Loyalty:BaseUrl"].ToString();
            AppSettingsProvider.ChargeCredits = Configuration["Upope.Loyalty:ChargeCredits"].ToString();
            AppSettingsProvider.AddCredits = Configuration["Upope.Loyalty:AddCredits"].ToString();
        }
    }
}
