using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperManager
{
    public class JasperConfig
    {
        private string domain { get; set; }
        private int port { get; set; }
        private string serverName { get; set; }
        private string baseURL { get; set; }

        /// <summary>
        /// Armazenar as informações do servidor jasper
        /// </summary>
        /// <param name="Domain">Dominio do servidor ex.: localhost ou www.myserver.jasper.com</param>
        /// <param name="Port">Porta de comunicação com o servidor. ex.: 8080</param>
        /// <param name="ServerName">Nome da instancia do servidor do jasper, ex.: jasperserver</param>
        public JasperConfig(string Domain, int Port, string ServerName)
        {
            domain = Domain;
            port = Port;
            serverName = ServerName;

            GerenateUrlBase();
        }

        /// <summary>
        /// Armazenar as informações do servidor jasper
        /// </summary>
        /// <param name="Domain">Dominio do servidor ex.: localhost ou www.myserver.jasper.com</param>
        public JasperConfig(string Domain)
        {
            // TODO: Complete member initialization
            domain = Domain;
            port = 8080;
            serverName = "jasperserver";

            GerenateUrlBase();
        }

        private void GerenateUrlBase()
        {
            baseURL = string.Format("http://{0}:{1}/{2}/rest_v2/", domain, port, serverName);
        }

        /// <summary>
        /// Responsavel por montar a url basica para comunicação com o servidor jasper
        /// </summary>
        /// <returns>retorna a url formatada de acordo com os paramentros informados no contrutor.</returns>
        public string GetBaseUrl()
        {
            return baseURL;
        }
    }
}
