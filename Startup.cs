using System.Linq;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApplication
{
    class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseBrowserLink();

            app.UseStaticFiles();

            app.UseStatusCodePagesWithRedirects("/{0}.html");

            app.Map("/mvc", mvcapp =>
            {
                mvcapp.UseMvcWithDefaultRoute();
            });

            app.Run(async c =>
            {
                var parameters = string.Join("\n",
                    c.Request.Query.Select(s => string.Format(
                        CultureInfo.InvariantCulture, "{0} = {1}", s.Key, s.Value))
                );
                
                await c.Response.WriteAsync(parameters);
            });
        }
    }
}