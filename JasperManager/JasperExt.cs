using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
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

        /// <summary>
        /// Serializar Objetos
        /// </summary>
        /// <param name="Obj">Objeto a ser seriallizado</param>
        /// <returns>string em json</returns>
        internal static string ToJson(this object Obj)
        {
            string Json = string.Empty;

            foreach (var property in Obj.GetType().GetProperties())
            {
                string key = property.Name.ToCamelCase();
                object value = property.GetValue(Obj);

                string Textvalue = string.Empty;

                if (value != null)
                {
                    Textvalue = value.ToString();

                    if (property.PropertyType == typeof(Nullable<bool>)) // java serializa boolean apenas se estiver lowerCase
                        Textvalue = Textvalue.ToLower();

                    if (property.PropertyType == typeof(Nullable<DateTime>))
                    {
                        DateTimeFormatInfo info = CultureInfo.CreateSpecificCulture("en").DateTimeFormat;
                        Textvalue = ((DateTime)value).ToString(info);
                    }

                    if (property.PropertyType.FullName == "System.Object")
                    {
                        Textvalue = value.ToString();
                        Json += string.Format(",\"{0}\": {1} ", key, Textvalue);
                    }
                    else
                        Json += string.Format(",\"{0}\":\"{1}\"", key, Textvalue);
                }
            }

            if (string.IsNullOrWhiteSpace(Json))
                return string.Empty;

            return "{" + Json.Substring(1) + "}";
        }

        internal static string ToCamelCase(this string texto)
        {
            string firstLetter = texto.Substring(0, 1);
            return firstLetter.ToLower() + texto.Substring(1);
        }
    }
}
