using System;
using System.IO;
using System.Reflection;
using HeroicBrawlServer.DAL.Repositories;
using HeroicBrawlServer.DAL.Repositories.Abstractions;
using HeroicBrawlServer.Services;
using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Services.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HeroicBrawlServer", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSignalR();

            // Db context
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));

            // Services
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IUserService, UserService>();

            // Repositories
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Options
            services.Configure<SecurityOptions>(Configuration.GetSection("SecurityOptions"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HeroicBrawlServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<RealtimeHub>("/api/rooms/hub");
                endpoints.MapControllers();
            });
        }
    }
}
