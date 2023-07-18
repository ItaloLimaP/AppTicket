using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppTicket
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var usuario = BaseUsuarios.Usuarios().FirstOrDefault(x => x.Email == context.UserName && x.Contacto == context.Password);

            if (usuario == null)
            {
                context.SetError("Invalid_grant", "Utilizador não encontrado ou password incorreto.");
                return;
            }
            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                "Username", context.UserName
                }
            });

            var identy = new ClaimsIdentity(context.Options.AuthenticationType);
            var identidadeUsuario = new AuthenticationTicket(identy, props);

            foreach (var funcao in usuario.Funcoes)
            {
                identidadeUsuario.Identity.AddClaim(new Claim(ClaimTypes.Role, funcao));
            }

                context.Validated(identidadeUsuario);            
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            //return base.TokenEndpoint(context);
            foreach (var item in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(item.Key, item.Value);
            }

            var claims = context.Identity.Claims.GroupBy(x => x.Type)
                .Select(y => new { Claim = y.Key, Value = y.Select(z => z.Value).ToArray() });

            foreach (var item in claims)
            {
                context.AdditionalResponseParameters.Add(item.Claim, JsonConvert.SerializeObject(item.Value));
            }

            return base.TokenEndpoint(context);
        }
    }
}