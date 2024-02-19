using Asp.Versioning;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ReportEvcn.Api
{
    public static class Startup
    {
        /// <summary>
        /// Подключение Swagger
        /// </summary>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddApiVersioning()
                .AddApiExplorer(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version= "v1",
                    Title = "ReportEvcn.Api",
                    Description = "This is version 1.0 of my pet project web-api",
                    TermsOfService = new Uri("https://t.me/esviken"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Telegram",
                        Url = new Uri("https://t.me/esviken")
                    }
                });

                options.SwaggerDoc("v2", new OpenApiInfo()
                {
                    Version = "v2",
                    Title = "ReportEvcn.Api",
                    Description = "This is version 2.0 of my pet project web-api",
                    TermsOfService = new Uri("https://t.me/esviken"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Telegram",
                        Url = new Uri("https://t.me/esviken")
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Введите, пожалуйста, валидный токен",
                    Name = "Авторизация",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        Array.Empty<string>()
                    }
                });
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,xmlFileName));
            });
        }
    }
}
