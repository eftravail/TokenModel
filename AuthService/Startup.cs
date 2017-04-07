using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AuthService.Startup))]

namespace AuthService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}
