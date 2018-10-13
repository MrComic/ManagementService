using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityServer4.AccessTokenValidation;
using ManagementService.Model.DbSets.Roles;
using ManagementService.Model.DbSets.User;
using ManagementService.Web.Controllers;
using ManagementService.Web.ViewModels;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using ManagementService.Service;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace ManagementService.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiSettings>(options => Configuration.GetSection("ApiSettings").Bind(options));
            

            services.AddAntiforgery(x =>  x.HeaderName = "X-XSRF-TOKEN" );
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
            {
                options.Audience = "Any";
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "http://localhost:44330/",
                    ValidAudience = "http://localhost:44330/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VeryStrongKey#123"))
                };

            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Lockout = new LockoutOptions() { DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5), MaxFailedAccessAttempts = 5 };
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "/Ui";
            });

            //services.addau;
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(new DisableRequestSizeLimitAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            var filePath = Path.Combine(System.AppContext.BaseDirectory, "ManagementService.Web.xml");
            services.AddSwaggerGen(c =>
            {
                c.MapType<Guid>(() => new Schema { Type = "string", Format = "uuid" });
                c.IncludeXmlComments(filePath);
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new Info { Title = "مستندات پروژه", Version = "v1" });
            });


            var builder = new ContainerBuilder();
                
            services.AddScoped<IAntiForgeryCookieService, AntiForgeryCookieService>();
            services.AddScoped<ITokenStoreService, TokenStoreService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Populate(services);
            builder.RegisterModule(new ManagementService.Service.DiRegister());
            return new AutofacServiceProvider(builder.Build());
        }


        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IAntiforgery antiforgery, IAntiForgeryCookieService antiForgeryCookieService)
        {
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole(   this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Trace);
            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMvc();


            //app.UseExceptionHandler(appBuilder =>
            //{
            //    appBuilder.Use(async (context, next) =>
            //    {
            //        var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
            //        if (error != null && error.Error is SecurityTokenExpiredException)
            //        {
            //            context.Response.StatusCode = 401;
            //            context.Response.ContentType = "application/json";
            //            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            //            {
            //                State = 401,
            //                Msg = "token expired"
            //            }));
            //        }
            //        else if (error != null && error.Error != null)
            //        {
            //            context.Response.StatusCode = 500;
            //            context.Response.ContentType = "application/json";
            //            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            //            {
            //                State = 500,
            //                Msg = error.Error.Message
            //            }));
            //        }
            //        else
            //        {
            //            await next();
            //        }
            //    });
            //});

            // app.UseAntiforgeryTokens();


            //app.Use(next => context =>
            //{
            //    var logger = loggerFactory.CreateLogger("ValidRequestMW");
            //    //logger.LogInformation("Request Cookie is " + context.Request.Cookies["XSRF-TOKEN"]);
            //    //logger.LogInformation("Request Header is " + context.Request.Headers["X-XSRF-TOKEN"]);

            //    string path = context.Request.Path.Value;
            //    if (
            //string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
            //string.Equals(path, "/index.html", StringComparison.OrdinalIgnoreCase))
            //    {


            //    //    antiForgeryCookieService.DeleteAntiForgeryCookies();
            //            var tokens = antiforgery.GetAndStoreTokens(context);
            //            context.Response.Cookies.Append(
            //                "XSRF-TOKEN",
            //                tokens.RequestToken,
            //                new CookieOptions() { HttpOnly = false }
            //            );
            //    }
            //    return next(context);

            //});

            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.EnableFilter();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "نسخه 1");
            });


            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Ui";

               

                //spa.UseSpaPrerendering(options =>
                //{
                //    options.BootModulePath = $"{spa.Options.SourcePath}/dist/server/main.js";
                //    options.BootModuleBuilder = env.IsDevelopment()
                //        ? new AngularCliBuilder(npmScript: "build:ssr")
                //        : null;
                //});

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

       
        }
    }
}
