using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MockBookingSystem.Core;
using MockBookingSystem.DAL;
using MockBookingSystem.Infrastructure;
using MockBookingSystem.Middlewares;
using MockBookingSystem.ServiceLayer.Contracts;
using MockBookingSystem.ServiceLayer.Factories;
using MockBookingSystem.ServiceLayer.Implementations;
using MockBookingSystem.ServiceLayer.Wrappers;
using MockBookingSystem.TripxClient;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;

namespace MockBookingSystemAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            InstallSwagger(builder);

            InstallAuthentication(builder);

            InstallRegistrables(builder);

            InstallApplication(builder);

            InstallInfrastructure(builder);

            builder.Services.Configure<ClientSettings>(builder.Configuration);

            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            var app = builder.Build();

            var registeredTypes = app.Services.GetRequiredService<IEnumerable<IRegistrable>>();

            foreach (var registeredType in registeredTypes)
            {
                await registeredType.RegisterAsync();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void InstallApplication(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IDestinationOptionService, DestinationOptionService>();
            builder.Services.AddSingleton<IDestinationSearchHelperFactory, DestinationSearchHelperFactory>();
            builder.Services.AddSingleton<IBookingService, BookingService>();
        }

        private static void InstallInfrastructure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IDAL, InMemoryDAL>();
            builder.Services.AddSingleton<IAccountService, AccountService>();
            builder.Services.AddSingleton<ITripxClient, TripxClient>();
            builder.Services.AddHttpClient<ITripxClient, TripxClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetSection("TripxAPI").Value);
            });
        }

        private static void InstallRegistrables(WebApplicationBuilder builder)
        {
            var registrables = typeof(InMemoryDAL).Assembly.GetTypes()
                .Where(x => typeof(IRegistrable).IsAssignableFrom(x));

            foreach (var registrable in registrables)
            {
                builder.Services.Add(new(typeof(IRegistrable), registrable, ServiceLifetime.Singleton));
            }
        }

        private static void InstallSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        private static void InstallAuthentication(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(builder.Configuration.GetSection("JwtTokenKey").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}