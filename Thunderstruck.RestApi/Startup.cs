using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;
using KestrelServerOptionsSystemdExtensions = Microsoft.AspNetCore.Hosting.KestrelServerOptionsSystemdExtensions;

namespace Thunderstruck.RestApi
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
            services.AddMvc()
                .AddMvcOptions(options => { options.EnableEndpointRouting = false; })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddSpotify(options =>
                {
                    options.ClientId = "f3fa527a095f46d0a1b70a344978a5d5";
                    options.ClientSecret = "6e31e86358e843b69cd07ff15139376a";
                    options.SaveTokens = true;
                    options.Scope.Add("playlist-modify-private");
                    options.Scope.Add("playlist-modify-public");
                    options.Scope.Add("user-read-currently-playing");
                    options.Scope.Add("user-read-email");
                    options.Scope.Add("app-remote-control");
                    options.CallbackPath = "/callback";
                    //scopes
                    //var end = options.AuthorizationEndpoint;
                    //options.AuthorizationEndpoint; //+= "&scope=user-read-private playlist-modify-private";
                });
           
            // source: https://github.com/NetTopologySuite/NetTopologySuite.IO.GeoJSON/wiki/Using-NTS.IO.GeoJSON4STJ-with-ASP.NET-Core-MVC
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    var geoJsonConverterFactory = new GeoJsonConverterFactory();
                    options.JsonSerializerOptions.Converters.Add(geoJsonConverterFactory);
                });
            services.AddControllers(options =>
                options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(Point))));

            services.AddSingleton(NtsGeometryServices.Instance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseMvc(routes => { routes.MapRoute("default", "api/{controller}/{action}"); });
        }
    }
}

