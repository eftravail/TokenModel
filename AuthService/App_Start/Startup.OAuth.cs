using AuthService.Core;
using AuthService.Formats;
using AuthService.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;

namespace AuthService
{
    public partial class Startup
    {
        private void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["issuer"];
            var secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]); //TODO: Add a salt value here as well?

            app.CreatePerOwinContext(() => new UsersContext());
            app.CreatePerOwinContext(() => new UsersManager());


            ////Sets JWT Token validation rules
            //var tokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters()
            //{
            //    ClockSkew = new TimeSpan(0, 5, 0)
            //};


            //Enables the JWT Token
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                //TokenValidationParameters = tokenValidationParameters,
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[] { new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret) }
            });


            // Get timespan from configuration or set default if not available
            int timespan;
            int.TryParse(ConfigurationManager.AppSettings["TimespanInMinutes"], out timespan);

            if (timespan == 0)
                timespan = 30;


            //Enables the OAuth Endpoint
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
#if DEBUG
                AllowInsecureHttp = true,
#endif
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(timespan),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(issuer),
            });
        }
    }
}