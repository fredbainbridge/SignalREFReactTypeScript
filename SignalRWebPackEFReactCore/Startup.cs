using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SignalRWebPack.Hubs;
using SignalRWebPack.Data;

namespace SignalRWebPack
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                services.AddDbContext<SignalRContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("azureConnection")));
            else
                services.AddDbContext<SignalRContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSignalR();
            services.AddMvcCore().AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            }
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
                var context = serviceScope.ServiceProvider.GetRequiredService<SignalRContext>();
                context.Database.Migrate();
            }
            /*
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
            */
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSignalR(options => {
                options.MapHub<ChatHub>("/hub");
            });
        }
    }
}
