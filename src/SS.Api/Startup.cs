using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SS.Api.Middleware;
using SS.Api.Middleware.Exceptions;
using SS.Api.Modules;
using SS.Users.Infrastructure.Configuration;
using SS.Users.Infrastructure.Validators;
using System.Text;
using FluentValidation.AspNetCore;
using SS.Collections.Infrastructure.Configuration;
using SS.Api.Modules.OrganizationApi;
using SS.Organizations.Infrastructure.Configuration;
using SS.Organizations.Infrastructure.Validators;
using Serilog;
using Serilog.Formatting.Compact;

namespace SS.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            ConfigureLogger();
            Configuration = configuration;
        }
        private static ILogger _logger;
        private static ILogger _loggerForApi;

        public IConfiguration Configuration { get; }
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IExceptionHandler, ExceptionHandler>();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            services.AddCors(cfg =>
            {
                cfg.AddPolicy("CoreClient",
                policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            var key = Encoding.ASCII.GetBytes(Configuration["Auth:SecretKey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddMvc()
            .AddFluentValidation(fv => {
                fv.RegisterValidatorsFromAssemblyContaining<UserRegisterValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<AddOrganizationValidator>();
                });
            
        }
        
        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new UserModuleAutofac());
            builder.RegisterModule(new CollectionsModuleAutofac());
            builder.RegisterModule(new OrganizationAutofacModule());
            UserModuleStartup.Initialize(Configuration["Database:ConnectionString"], Configuration["Database:DbName"], Configuration["Auth:SecretKey"], _logger);
            CollectionsStartup.Initialize(Configuration["Database:ConnectionString"], Configuration["Database:DbName"], _logger);
            OrganizationModuleStartup.Initialize(Configuration["Database:ConnectionString"], Configuration["Database:DbName"], _logger);
        }

        private void ConfigureLogger()
        {
            _logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.RollingFile(new CompactJsonFormatter(), "logs/logs")
                .CreateLogger();

            _loggerForApi = _logger.ForContext("Module", "API");

            _loggerForApi.Information("Logger configured");
        }


        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseExceptionMiddleware();

            app.UseSwagger();

            app.UseCors("CoreClient");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
