
using KwestKarz.Entities;
using KwestKarz.Services;
using KwestKarz.Services.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace KwestKarz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KwestKarz API",
                    Version = "v1"
                });

                c.MapType<IFormFile>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Paste your JWT token here with Bearer prefix. Example: Bearer eyJ..."
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Paste your JWT token here with Bearer prefix. Example: Bearer eyJ..."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, Array.Empty<string>() }
                });
            });


            builder.Configuration
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets<Program>(optional: true)
                    .AddEnvironmentVariables();


            builder.Services.AddDbContext<KwestKarzDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("KwestKarzDb")));

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                    };
                });

            builder.Services.AddAuthorization(); // make sure this is also in place

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials(); // If using cookies or auth headers
                    });
            });


            //////////////////////////////////////////////
            /// system service
            //////////////////////////////////////////////

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITripEarningsService, TripEarningsService>();
            builder.Services.AddScoped<ICSVParserService, CSVParserService>();
            builder.Services.AddScoped<IVehicleEventService, VehicleEventService>();
            builder.Services.AddScoped<IVehicleImportService, VehicleImportService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<IGuestService, GuestService>();
            builder.Services.Configure<GoogleEmailSettings>(builder.Configuration.GetSection("google"));
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<ILogService, LogService>();


            ////////////////////////////////////////////
            /// Build
            ////////////////////////////////////////////

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5066);    // HTTP
                options.ListenAnyIP(7102, listenOptions =>
                {
                    listenOptions.UseHttps();  // HTTPS
                });
            });

            var app = builder.Build();


            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEPTION:");
                    Console.WriteLine(ex.Message);
                    throw;
                }
            });

            /////////////////////////////////////////////
            /// Database Setup
            /////////////////////////////////////////////

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<KwestKarzDbContext>();
                DbSeeder.SeedRoles(dbContext);
                DbSeeder.SeedAdminUser(dbContext);
            }

            app.UseAuthentication();
            app.UseAuthorization();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection();
            }

           
            app.UseCors(MyAllowSpecificOrigins);

            app.MapControllers();

            app.Run();
        }
    }
}
