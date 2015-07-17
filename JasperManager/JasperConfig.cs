using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperManager
{
    public class JasperConfig
    {
        private string Domain { get; set; }
        private int Port { get; set; }
        private string ServerName { get; set; }
        private string BaseURL { get; set; }

        /// <summary>
        /// Armazenar as informações do servidor jasper
        /// </summary>
        /// <param name="Domain">Dominio do servidor ex.: localhost ou www.myserver.jasper.com</param>
        /// <param name="Port">Porta de comunicação com o servidor. ex.: 8080</param>
        /// <param name="ServerName">Nome da instancia do servidor do jasper, ex.: jasperserver</param>
        public JasperConfig(string Domain, int Port, string ServerName)
        {
            this.Domain = Domain;
            this.Port = Port;
            this.ServerName = ServerName;

            GerenateUrlBase();
        }

        /// <summary>
        /// Armazenar as informações do servidor jasper
        /// </summary>
        /// <param name="Domain">Dominio do servidor ex.: localhost ou www.myserver.jasper.com</param>
        public JasperConfig(string Domain)
        {
            // TODO: Complete member initialization
            this.Domain = Domain;
            Port = 8080;
            ServerName = "jasperserver";

            GerenateUrlBase();
        }

        private void GerenateUrlBase()
        {
            BaseURL = string.Format("http://{0}:{1}/{2}/rest_v2/", Domain, Port, ServerName);
        }

        /// <summary>
        /// Responsavel por montar a url basica para comunicação com o servidor jasper
        /// </summary>
        /// <returns>retorna a url formatada de acordo com os paramentros informados no contrutor.</returns>
        public string GetBaseUrl()
        {
            return BaseURL;
        }
    }
}
