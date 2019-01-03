using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WS.FrontEnd.WebApi.App_Start;
using WS.FrontEnd.WebApi.Authentication.MIddleware;
using WS.FrontEnd.WebApi.Authentication.Providers;
using WS.FrontEnd.WebApi.Mappings;

[assembly: OwinStartup(typeof(WS.FrontEnd.WebApi.Startup))]
namespace WS.FrontEnd.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = GlobalConfiguration.Configuration;

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            app.UseCors(CorsOptions.AllowAll);

            app.Use<InvalidAuthenticationMiddleware>();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AreaRegistration.RegisterAllAreas();

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RunCustomInitializations(config);

            app.UseWebApi(config);

            config.EnsureInitialized();
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private void RunCustomInitializations(HttpConfiguration config)
        {
            AutoMapperConfig.Init();
            config.Services.Replace(typeof(IHttpControllerActivator), new ServiceActivator());
        }
    }
}
