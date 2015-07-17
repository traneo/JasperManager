using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperManager
{
    internal static class JasperExt
    {
        /// <summary>
        /// Converter objeto em queryString e concatenar com o endereço informado.
        /// </summary>
        /// <param name="Endereco">Endereço base</param>
        /// <param name="Obj">class/objeto com propriedades a serem convertidas</param>
        /// <returns>coleção de dados do tipo chave/valor</returns>
        internal static NameValueCollection ToQueryString(this object Obj)
        {
            NameValueCollection query = new NameValueCollection();

            foreach (var property in Obj.GetType().GetProperties())
            {
                string key = property.Name;
                string value = property.GetValue(Obj).ToString();

                query.Add(key, value);
            }

            return query;
        }

        internal static string ToJson(this object Obj)
        {
            string Json = string.Empty;

            foreach (var property in Obj.GetType().GetProperties())
            {
                string key = property.Name.ToLower();
                object value = property.GetValue(Obj);

                string Textvalue = string.Empty;

                if (value != null)
                {
                    Textvalue = value.ToString();

                    if (property.PropertyType == typeof(bool)) // java serializa boolean apenas se estiver lowerCase
                        value = Textvalue.ToLower();

                    Json += string.Format(",\"{0}\":\"{1}\"", key, Textvalue);
                }
            }

            return "{" + Json.Substring(1) + "}";
        }
    }
}
