
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperManager
{
    public class JasperResult
    {
        private byte[] arquivo;
        private string Endereco;
        private object Parametros;
        private JasperReportFormat JasperReportFormat;

        public JasperResult(byte[] arquivo, string Endereco, object Parametros, JasperReportFormat JasperReportFormat)
        {
            // TODO: Complete member initialization
            this.arquivo = arquivo;
            this.Endereco = Endereco;
            this.Parametros = Parametros;
            this.JasperReportFormat = JasperReportFormat;
        }
        /// <summary>
        /// Retorna documento obtido na requisição ao jasper
        /// </summary>
        /// <returns>array de bytes que representa o arquivo binario solicitado ao jasper</returns>
        public byte[] GetDocumento()
        {
            return arquivo;
        }
        /// <summary>
        /// Montar o nome do documento baseado no tipo de relatorio requisitado
        /// </summary>
        /// <param name="FileName">nome do documento</param>
        /// <returns>nome do documento mais extensão.</returns>
        public string DefineFileName(string FileName)
        {
            return string.Format("{0}.{1}", FileName, JasperReportFormat.ToString().ToLower());
        }

        /// <summary>
        /// Obtem o Content-Type adequado a solicitação do relatorio.
        /// </summary>
        /// <returns>application/x , sendo x = pdf, xml, doc, etc.</returns>
        public string GetJasperContentType()
        {
            return string.Format("application/{0}", JasperReportFormat.ToString());
        }
    }
}
