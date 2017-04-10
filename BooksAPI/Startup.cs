using BooksAPI.Core;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BooksAPI.Startup))]

namespace BooksAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new BooksContext());

            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}
