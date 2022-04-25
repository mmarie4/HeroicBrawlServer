using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HeroicBrawlServer.Controllers.Middleware;
using HeroicBrawlServer.Data.Repositories;
using HeroicBrawlServer.Data.Repositories.Abstractions;
using HeroicBrawlServer.Services;
using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Services.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Configuration;

namespace HeroicBrawlServer
{
    public class Startup
    {

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                    {
                        options.Filters.Add<ExceptionFilter>();
                    })
                    .AddNewtonsoftJson(options =>
                     {
                         options.SerializerSettings.ContractResolver = new DefaultContractResolver
                         {
                             NamingStrategy = new SnakeCaseNamingStrategy()
                         };
                         options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                         options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                     });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HeroicBrawlServer", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Add SignalR
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.SetIsOriginAllowed(origin => true)
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });


            // Configure JWT authentication.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["SecurityOptions:Issuer"],
                      ValidAudience = Configuration["SecurityOptions:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityOptions:Secret"])),
                      RequireExpirationTime = false
                  };

                  options.Events = new JwtBearerEvents
                  {
                      OnAuthenticationFailed = context =>
                      {
                          if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                          {
                              context.Response.Headers.Add("Token-Expired", "true");
                          }
                          return Task.CompletedTask;
                      }
                  };
              });

            // Db context
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));

            // Services
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISettingsService, SettingsService>();

            // Hosted Services
            services.AddHostedService<CleanerHostedService>();
            services.AddHostedService<GameLoopHostedService>();

            // Repositories
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Options
            services.Configure<SecurityOptions>(Configuration.GetSection("SecurityOptions"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HeroicBrawlServer v1"));
            }

            loggerFactory.AddSerilog();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<RealtimeHub>("/api/rooms/hub");
                endpoints.MapControllers();
            });
        }
    }
}
