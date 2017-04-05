using Newtonsoft.Json.Serialization;
using System.Data.Entity;
using System.Web.Http;

namespace BooksAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Configure Web API
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Restrict access to all endpoints (except the OAuth endpoint) to requests that have been authenticated
            GlobalConfiguration.Configure(FilterConfig.Configure);

            // Tell the web application about the Entity Framework intializer
            Database.SetInitializer(new Core.EntityFramework.Initializer());

            // Configure JSON
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();


        }
    }
}
