using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperManager
{
    /*
        {
            "reportUnitUri": "/reports/interactive/CustomersReport" ,
            "async": "true",
            "freshData": "false",
            "saveDataSnapshot": "false",
            "outputFormat": "pdf",
            "interactive": "false"
        }
     * */

    internal class JasperExecutionRequest
    {
        internal string ToJson()
        {
            string Json = string.Empty;

            foreach (var property in this.GetType().GetProperties())
            {
                string key = property.Name;
                string value = property.GetValue(this).ToString();

                if (property.PropertyType == typeof(bool)) // java serializa boolean apenas se estiver lowerCase
                    value = value.ToLower();

                Json += string.Format(",\"{0}\":\"{1}\"", key, value);
            }

            return "{" + Json.Substring(1) + "}";
        }

        public string reportUnitUri { get; set; }
        public bool async { get; set; }

        public bool freshData { get; set; }

        public bool saveDataSnapshot { get; set; }

        public string outputFormat { get; set; }

        public bool interactive { get; set; }

        public bool ignorePagination { get; set; }
    }
}
