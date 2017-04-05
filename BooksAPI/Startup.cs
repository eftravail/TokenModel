using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BooksAPI.Startup))]

namespace BooksAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }
    }
}
