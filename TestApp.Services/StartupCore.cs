using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestApp.DataAccess.Repositories;
using TestApp.DataAccess.RepositoryInterfaces;
using TestApp.DataContext;
using TestApp.DataContext.Entities;
using TestApp.Services.ErrorHandling;
using TestApp.Services.Middleware;

namespace TestApp.Services
{
    public class StartupCore
    {
        const string CorsPolicyName = "TestAppCORSPolicy";

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        x => x.MigrationsAssembly("TestApp.DataContext"))
                    );


            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<IProvincesRepository, ProvincesRepository>();
            services.AddScoped<IUserRegistrationService, UserRegistrationService>();
            services.AddScoped<IGeoService, GeoService>();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/browser";
            });


            services.AddControllers();

            string? allowedOrigins = configuration.GetValue<string>("AllowedOrigins") ?? "";

            if (!string.IsNullOrWhiteSpace(allowedOrigins))
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(CorsPolicyName,
                        builder => builder.WithOrigins(allowedOrigins.Split(","))
                        .AllowAnyMethod()
                        .AllowAnyHeader());
                });
            }
        }
        public static void Configure(WebApplication app)
        {
            app.MapControllers();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(CorsPolicyName);

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp/dist/browser";

                spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        if (ctx.Context.Request.Path.StartsWithSegments("/api"))
                        {
                            // Don't process requests to API by SPA middleware.
                            ctx.Context.Response.StatusCode = 404;
                            ctx.Context.Response.ContentLength = 0;
                            ctx.Context.Response.Body = Stream.Null;
                        }
                    }
                };
            });

            app.UseExceptionHandler(app => app.Run(HandleExceptionHandler.HandleExceptionAsync));
            app.UseMiddleware<AuditMiddleware>();
        }

        public static void InitializeDB(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                bool isMigrationNeeded = dbContext.Database.GetPendingMigrations().Any();

                if (isMigrationNeeded)
                {
                    dbContext.Database.Migrate();
                }
            }
        }
    }
}
