using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JasperManager
{
    public class JasperResultAsync
    {
        private string Response;
        private System.Net.WebHeaderCollection webHeaderCollection;

        public JasperResultAsync(string Response, System.Net.WebHeaderCollection webHeaderCollection)
        {
            // TODO: Complete member initialization
            this.Response = Response;
            this.webHeaderCollection = webHeaderCollection;
        }

        public string GetCookieData()
        {
            return webHeaderCollection["Set-Cookie"];
        }

        public string GetResponse()
        {
            return Response;
        }

        public string GetDocumento()
        {
            throw new NotImplementedException();
        }
    }
}
