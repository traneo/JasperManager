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
    }
}
