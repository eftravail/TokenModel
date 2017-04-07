using BooksAPI.Core;
//using BooksAPI.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Configuration;
using System.Threading.Tasks;

namespace BooksAPI
{
    public partial class Startup
    {
        private void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["issuer"];
            //var audience = ConfigurationManager.AppSettings["audience"];
            var secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);

            app.CreatePerOwinContext(() => new BooksContext());

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                //AllowedAudiences = new[] { audience },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[] { new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret) },
                Provider = new OAuthBearerAuthenticationProvider
                {
                    OnValidateIdentity = context =>
                    {
                        context.Ticket.Identity.AddClaim(new System.Security.Claims.Claim("newCustomClaim", "newValue"));
                        return Task.FromResult<object>(null);
                    }
                }
            });
        }
    }
}
