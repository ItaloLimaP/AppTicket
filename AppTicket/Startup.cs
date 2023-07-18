using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(AppTicket.Startup))]

namespace AppTicket
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
            {
                var config = new HttpConfiguration();

                config.MapHttpAttributeRoutes();

                config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = 
                    new CamelCasePropertyNamesContractResolver();

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );

                config.EnableSwagger(c => {
                    c.SingleApiVersion("v1", "AppTicket");
                    c.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + @"\bin\AppTicket.xml");
                });

                app.UseCors(CorsOptions.AllowAll);

                AtivandoAcessTokens(app);

                app.UseWebApi(config);
            }

        private void AtivandoAcessTokens(IAppBuilder app)
        {
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new ProviderDeTokensDeAcesso()
            };

            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    } 
}
