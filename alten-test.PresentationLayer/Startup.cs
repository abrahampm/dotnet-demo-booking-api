using Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using alten_test.BusinessLayer.Interfaces;
using alten_test.BusinessLayer.Services;
using alten_test.BusinessLayer.Utilities;


namespace alten_test.PresentationLayer
{
    public class Startup
    {
        private const string AppCorsPolicy = "AllowAppFrontendCorsPolicy";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // Configure Unity Container
        public void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType(
                typeof(IContactService),
                typeof(ContactService),
                null,
                TypeLifetime.Hierarchical);
            container.RegisterType(
                typeof(IReservationService), 
                typeof(ReservationService), 
                null, 
                TypeLifetime.Hierarchical);
            container.RegisterType(
                typeof(IRoomService), 
                typeof(RoomService), 
                null, 
                TypeLifetime.Hierarchical);
            container.AddNewExtension<DependencyInjectionExtension>();
            container.AddExtension(new Diagnostic());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(name: AppCorsPolicy,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200", "https://localhost:4200");
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                })
            );
            
            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate();
            
            services.AddControllers().AddControllersAsServices();;
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "alten_test.PresentationLayer", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "alten_test.PresentationLayer v1"));
            }

            if (env.IsProduction())
            {
                app.UseHttpsRedirection();    
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
