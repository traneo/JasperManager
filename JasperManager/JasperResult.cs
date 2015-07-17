
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperManager
{
    public class JasperResult
    {
        private byte[] File;
        private string Url;
        private object Param;
        private JasperReportFormat JasperReportFormat;
        private JasperStatus Status;
        private JasperDescriptor Descriptor;
        private string MessageDetails;

        public JasperResult(byte[] File, string Url, object Param, JasperReportFormat JasperReportFormat)
        {
            // TODO: Complete member initialization
            this.File = File;
            this.Url = Url;
            this.Param = Param;
            this.JasperReportFormat = JasperReportFormat;
        }

        public JasperResult(string Url, object Param, JasperStatus Status)
        {
            this.File = null;
            this.Url = Url;
            this.Param = Param;
            this.Status = Status;
        }

        public JasperResult(string Url, JasperDescriptor Descriptor, JasperStatus Status, string MessageDetails)
        {
            // TODO: Complete member initialization
            this.Url = Url;
            this.Descriptor = Descriptor;
            this.Status = Status;
            this.MessageDetails = MessageDetails;
        }

        /// <summary>
        /// Retorna documento obtido na requisição ao jasper
        /// </summary>
        /// <returns>array de bytes que representa o arquivo binario solicitado ao jasper</returns>
        public byte[] GetDocument()
        {
            return File;
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
        public string GetContentType()
        {
            return string.Format("application/{0}", JasperReportFormat.ToString());
        }

        public JasperStatus GetStatus()
        {
            return Status;
        }

        public string GetMessage()
        {
            return MessageDetails;
        }
    }
}
