using System;
using System.Collections.Generic;
using System.Text;
using Aiia.Sample.Helpers;
using Aiia.Sample.Modules;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;

namespace Aiia.Sample
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
                app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllerRoute(
                                                              "default",
                                                              "{controller}/{action=Index}/{id?}");
                             });

            app.UseSpa(spa =>
                       {
                           spa.Options.SourcePath = "App";

                           if (env.IsDevelopment())
                           {
                               spa.UseAngularCliServer("start");
                           }
                       });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<DatabaseModule>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var options = new SampleOptions();
            _configuration.Bind(options);

            services.AddDistributedMemoryCache();

            JsonConvert.DefaultSettings = () => NewtonsoftJsonSerializerProvider.Settings;
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddSingleton(_configuration);
            services.Configure<SampleOptions>(_configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(jwtOptions =>
                                  {
                                      jwtOptions.RequireHttpsMetadata = false;
                                      jwtOptions.SaveToken = true;
                                      jwtOptions.TokenValidationParameters = new TokenValidationParameters
                                                                             {
                                                                                 ValidateIssuerSigningKey = true,
                                                                                 IssuerSigningKeys = new List<SecurityKey>
                                                                                                     {
                                                                                                         new SymmetricSecurityKey(Encoding
                                                                                                                                  .UTF8
                                                                                                                                  .GetBytes(options.Security
                                                                                                                                                   .TokenKey)),
                                                                                                         new SymmetricSecurityKey(Encoding
                                                                                                                                  .UTF8
                                                                                                                                  .GetBytes(options.Security
                                                                                                                                                   .RefreshToken))
                                                                                                     },
                                                                                 ValidateIssuer = false,
                                                                                 ValidateAudience = false
                                                                             };
                                  });

            services.AddCors(opt => EnableCors(opt, options));


            services.AddResponseCompression(compressionOptions =>
                                            {
                                                compressionOptions.EnableForHttps = true;
                                                compressionOptions.Providers.Add<GzipCompressionProvider>();
                                                compressionOptions.Providers.Add<BrotliCompressionProvider>();
                                            });

            services.AddControllersWithViews().AddNewtonsoftJson(jsonOptions => jsonOptions.SerializerSettings.Configure());
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "App/dist"; });
        }

        private void EnableCors(CorsOptions options, SampleOptions internalOptions)
        {
            if (_environment.IsDevelopment())
                options.AddDefaultPolicy(builder =>
                                         {
                                             builder.WithOrigins(new Uri(internalOptions.SampleAppUrl).GetLeftPart(UriPartial.Authority),
                                                                 "https://localhost:5001",
                                                                 "http://localhost:5000")
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader()
                                                    .AllowCredentials();
                                         });
            else
                options.AddDefaultPolicy(builder =>
                                         {
                                             builder.WithOrigins(new Uri(internalOptions.SampleAppUrl).GetLeftPart(UriPartial.Authority))
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader()
                                                    .AllowCredentials();
                                         });
        }
    }
}
