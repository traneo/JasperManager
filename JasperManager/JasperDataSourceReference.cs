using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JasperManager
{
    public class JasperDataSourceReference
    {
        private string QueryResourceUri;

        public JasperDataSourceReference(string QueryResourceUri)
        {
            // TODO: Complete member initialization
            this.QueryResourceUri = QueryResourceUri;
        }

        public string GetQueryResourceUri()
        {
            return QueryResourceUri;
        }

        public override string ToString()
        {
            return "\"dataSourceReference\": {\"uri\":\"" + QueryResourceUri + "\"} ";
        }
    }
}
