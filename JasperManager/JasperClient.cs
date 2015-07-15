using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JasperManager
{
    public class JasperClient
    {
        private JasperConfig config;
        private JasperAuthorization auth;

        /// <summary>
        /// Obter as configurações necessarias para estabelecer conexão.
        /// </summary>
        /// <param name="config">configuração do servidor</param>
        /// <param name="auth">informações de autenticação</param>
        public JasperClient(JasperConfig config, JasperAuthorization auth)
        {
            // TODO: Complete member initialization
            this.config = config;
            this.auth = auth;
        }

        /// <summary>
        /// Efetuar download do relatório
        /// </summary>
        /// <param name="FileUrl">Local onde se encontra o relatório</param>
        /// <param name="Parametros">objeto com so paramentros necessários para executar o relatório.</param>
        /// <param name="JasperReportFormat">Formato desejado do relatório.</param>
        /// <returns>objeto contendo o documento e varias informações uteis para o download do mesmo.</returns>
        public JasperResult Get(string FileUrl, object Parametros, JasperReportFormat JasperReportFormat)
        {
            WebClient web = new WebClient();
            web.Credentials = new NetworkCredential(auth.GetUsuario(), auth.GetPassword());

            string Endereco = string.Format("{0}reports{1}.{2}", config.GetBaseUrl(), FileUrl, JasperReportFormat.ToString());

            web.QueryString = Parametros.ToQueryString();

            var arquivo = web.DownloadData(Endereco);

            return new JasperResult(arquivo, Endereco, Parametros, JasperReportFormat);
        }

        [Obsolete("Nao usar", true)]
        private JasperResultAsync GetAsync(string FileUrl, object Parametros, JasperReportFormat JasperReportFormat)
        {
            JasperExecutionRequest request = new JasperExecutionRequest();

            request.reportUnitUri = FileUrl;
            request.async = true;
            request.freshData = false;
            request.saveDataSnapshot = false;
            request.outputFormat = JasperReportFormat.ToString();
            request.interactive = false;
            request.ignorePagination = true;

            return GetAsync(FileUrl, Parametros, JasperReportFormat.PDF, request);
        }

        [Obsolete("Nao usar", true)]
        private JasperResultAsync GetAsync(string FileUrl, object Parametros, JasperReportFormat JasperReportFormat, JasperExecutionRequest RequestParam)
        {
            WebClient web = new WebClient();
            web.Credentials = new NetworkCredential(auth.GetUsuario(), auth.GetPassword());
            web.Headers.Add("Content-Type", "application/json");

            string Endereco = string.Format("{0}reportExecutions", config.GetBaseUrl());

            JasperExecutionRequest request = RequestParam;

            if (RequestParam == null)
            {
                request.reportUnitUri = FileUrl;
                request.async = false;
                request.freshData = false;
                request.saveDataSnapshot = false;
                request.outputFormat = JasperReportFormat.ToString();
                request.interactive = false;
            }

            string param = request.ToJson();
            var Response = web.UploadString(Endereco, "POST", param);

            return new JasperResultAsync(Response, web.ResponseHeaders);
        }
    }
}
