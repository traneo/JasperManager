using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperManager
{
    public class JasperAuthorization
    {
        private string username { get; set; }
        private string password { get; set; }

        /// <summary>
        /// Armazenar as informações de login
        /// </summary>
        /// <param name="Usarname">usuario</param>
        /// <param name="Password">senha</param>
        public JasperAuthorization(string Usarname,string Password )
        {
            username = Usarname;
            password = Password;
        }

        /// <summary>
        /// Obter nome do usuario armazenado.
        /// </summary>
        /// <returns>nome do usuario</returns>
        public string GetUsuario()
        {
            return username;
        }

        /// <summary>
        /// Obter senha armazenada
        /// </summary>
        /// <returns>senha do usuario</returns>
        public string GetPassword()
        {
            return password;
        }
    }
}
