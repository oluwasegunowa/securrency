using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TDS.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDS.Domain.Extensions
{
    public static class JWTServiceExtensions
    {

    //  IOptions<JWTSettings> settings
        public static void ConfigureJWTService(this IServiceCollection services, IConfiguration Config)
        {

           // if (settings.Value == null) throw new Exception("Authorization parameters has not been configured");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(cfg =>
            {

                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = Config.GetValue<bool>("JWTSettings:ValidateIssuerSigningKey"),// settings.Value.ValidateSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.GetValue<string >("JWTSettings:SecurityKey"))),
                    ValidateIssuer = Config.GetValue<bool>("JWTSettings:ValidateIssuer"),
                    ValidIssuer = Config.GetValue<string>("JWTSettings:Issuer"),
                    ValidateAudience = Config.GetValue<bool>("JWTSettings:ValidateAudience"),
                    ValidAudience = Config.GetValue<string>("JWTSettings:Audience"),
                    ValidateLifetime = Config.GetValue<bool>("JWTSettings:ValidateLifetime") 
                };
            });
        }
    }
}
