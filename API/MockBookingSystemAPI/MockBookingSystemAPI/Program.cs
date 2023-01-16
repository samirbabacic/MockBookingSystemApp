using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MockBookingSystem.Core;
using MockBookingSystem.DAL;
using MockBookingSystem.Infrastructure;
using MockBookingSystem.ServiceLayer.Contracts;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace MockBookingSystemAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


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

            // Add services to the container.
            var defaultAuthPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter(defaultAuthPolicy));
            });

            var registrables = typeof(InMemoryDAL).Assembly.GetTypes()
                .Where(x => typeof(IRegistrable).IsAssignableFrom(x));

            foreach (var registrable in registrables)
            {
                builder.Services.Add(new(typeof(IRegistrable), registrable, ServiceLifetime.Singleton));
            }

            builder.Services.AddSingleton<IDAL, InMemoryDAL>();
            builder.Services.AddSingleton<IAccountService, AccountService>();
            builder.Services.Configure<ClientSettings>(builder.Configuration);

            var app = builder.Build();

            var registeredTypes = app.Services.GetRequiredService<IEnumerable<IRegistrable>>();

            foreach(var registeredType in registeredTypes)
            {
                await registeredType.RegisterAsync();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}