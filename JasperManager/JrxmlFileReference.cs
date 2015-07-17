using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JasperManager
{
    public class JrxmlFileReference
    {
        private string JrxmlFileUri;

        public JrxmlFileReference(string JrxmlFileUri)
        {
            // TODO: Complete member initialization
            this.JrxmlFileUri = JrxmlFileUri;
        }

        public string GetJrxmlFileUri()
        {
            return JrxmlFileUri;
        }

        public override string ToString()
        {
            return "\"jrxmlFileReference\": {\"uri\":\"" + JrxmlFileUri + "\"} ";
        }
    }
}
