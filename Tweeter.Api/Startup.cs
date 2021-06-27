using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Tweeter.Api.Middlewares;
using Tweeter.Application.Extensions;
using Tweeter.Application.Helpers;
using Tweeter.DataAccess.MSSQL.Context;

namespace Tweeter.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TweeterContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("TweeterContext")));

            services.AddHttpContextAccessor();
            services.AddApplicationServices();
            services.AddIdentityServices();
            services.AddAutoMapper(typeof(AutoMapperExtension).Assembly);

            services.AddCors();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseMiddleware<ErrorHandlerMiddleware>();
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources/Images")),
                RequestPath = "/Resources/Images"
            });
            
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Resources/Images")),
                RequestPath = "/Resources/Images"
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
