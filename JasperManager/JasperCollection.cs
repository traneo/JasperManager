using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperManager
{
    public class JasperCollection<T> : List<T>
        where T : class
    {
        public override string ToString()
        {
            string itens = string.Join(",", this.Select(x => x.ToString()).ToArray());

            return "{ " + itens + " }";
        }
    }
}
