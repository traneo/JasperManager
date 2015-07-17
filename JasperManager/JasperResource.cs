using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JasperManager
{
    public class JasperResource
    {
        private string Name;
        private string File;
        private string Url;

        public JasperResource(string Name, string File)
        {
            // TODO: Complete member initialization
            this.Name = Name;
            this.File = File;
        }

        public JasperResource(string Url)
        {
            // TODO: Complete member initialization
            this.Url = Url;
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Url))
            {
                return "\"fileReference\": {\"uri\":\"" + Url + "\"} ";
            }

            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(File))
            {
                return "\"resource\": {\"name\":\"" + Name + "\", \"file\":\"" + File + "\"} ";
            }

            return string.Empty;
        }
    }
}
