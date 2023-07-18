using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppTicket
{
    public static class BaseUsuarios
    {

        public static IEnumerable<Usuario> Usuarios()
        {
            return new List<Usuario>
            {
            new Usuario { Email = "italo@teste", Contacto = "123", Funcoes = new string[] { Funcao.Admin} },
            new Usuario { Email = "naiane@teste", Contacto = "321", Funcoes = new string[] { Funcao.Cliente}}
            };
        }

        public class Usuario
        {
            public string Email { get; set; }
            public string Contacto { get; set; }
            public string[] Funcoes { get; set; }
        }

        public class Funcao
        {
            public const string Admin = "Admin";
            public const string Cliente = "Cliente";
        }
    }
}