﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReportEvcn.Domain.Settings;
using System.Reflection;
using System.Text;

namespace ReportEvcn.Api
{
    public static class Startup
    {

        /// <summary>
        /// Подключение аутентификации и авторизации
        /// </summary>
        public static void AddAuthenticationAndAuthorization(this IServiceCollection services, 
            WebApplicationBuilder builder)
        {
            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var options = builder.Configuration.GetSection(JwtSettings.DefaultSection).Get<JwtSettings>();
                var jwtKey = options.JwtKey;
                var issuer = options.Issuer;
                var audience = options.Audience;


                //o.Authority = options.Authority;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

            });
        }









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
