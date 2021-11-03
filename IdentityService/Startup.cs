using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Securrency.Identity.Application.Extensions;
using SecurrencyTDS.Domain.Authorization;
using SecurrencyTDS.Domain.Extensions;
using SecurrencyTDS.Domain.Logging;
using SecurrencyTDS.IdentityService.Infrastructure.Persistence;
using SecurrencyTDS.IdentityService.Infrastructure.Persistence.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace IdentityService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureLoggerService();
            services.ConfigureLoggerService();
            services.AddRepository(Configuration.GetConnectionString("DefaultConnection"));
            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
            services.RegisterRequestHandlers();




            services.AddSwaggerGen(c =>
            {




                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = $"Indentity  Microservice",
                    Version = "v1",
                    Description = "This service handles the authentication of users"
                });



                c.IncludeXmlComments(Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml"), true);
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Authorization format : Bearer {token}",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });


                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement() {

                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme =JwtBearerDefaults.AuthenticationScheme,// "Bearer",
                            Name = JwtBearerDefaults.AuthenticationScheme,//"Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });



            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger, ApplicationDbContext db)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            if (!db.Database.EnsureCreated())
            {
                db.Database.Migrate();
            }


            app.UseHttpsRedirection();

            app.ConfigureExceptionHandler(logger);

            app.UseRouting();
            app.UseSwagger();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(c =>
            {
                //c.RoutePrefix = "api-docs";
                c.SwaggerEndpoint("./v1/swagger.json", $" API V1");
            });


            
        }
    }
}
