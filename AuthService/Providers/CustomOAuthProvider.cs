﻿using AuthService.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //TODO: Other data checks we can do to validate?

            var user = context.OwinContext.Get<UsersContext>().Users.FirstOrDefault(u => u.UserName == context.UserName);

            if (!context.OwinContext.Get<UsersManager>().CheckPassword(user, context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "audience", (context.ClientId == null) ? string.Empty : context.ClientId }
                });

            var ticket = new AuthenticationTicket(SetClaimsIdentity(context, user), props);

            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            var audience = AudiencesStore.FindAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();

            return Task.FromResult<object>(null);
        }

        private static ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, IdentityUser user)
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));

            var userRoles = context.OwinContext.Get<UsersManager>().GetRoles(user.Id);
            foreach (var role in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }
    }
}