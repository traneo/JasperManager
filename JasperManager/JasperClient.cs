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
        private JasperConfig Config;
        private JasperAuthorization Auth;

        /// <summary>
        /// Obter as configurações necessarias para estabelecer conexão.
        /// </summary>
        /// <param name="Config">configuração do servidor</param>
        /// <param name="Auth">informações de autenticação</param>
        public JasperClient(JasperConfig Config, JasperAuthorization Auth)
        {
            // TODO: Complete member initialization
            this.Config = Config;
            this.Auth = Auth;
        }

        /// <summary>
        /// Efetuar download do relatório
        /// </summary>
        /// <param name="FileUrl">Local onde se encontra o relatório</param>
        /// <param name="Param">objeto com so paramentros necessários para executar o relatório.</param>
        /// <param name="JasperReportFormat">Formato desejado do relatório.</param>
        /// <returns>objeto contendo o documento e varias informações uteis para o download do mesmo.</returns>
        public JasperResult Get(string FileUrl, object Param, JasperReportFormat JasperReportFormat)
        {
            WebClient web = new WebClient();
            web.Credentials = new NetworkCredential(Auth.GetUsuario(), Auth.GetPassword());

            string url = string.Format("{0}reports{1}.{2}", Config.GetBaseUrl(), FileUrl, JasperReportFormat.ToString());

            web.QueryString = Param.ToQueryString();

            var responseFile = web.DownloadData(url);

            return new JasperResult(responseFile, url, Param, JasperReportFormat);
        }

        [Obsolete("Nao usar", true)]
        private JasperResultAsync GetAsync(string FileUrl, object Param, JasperReportFormat JasperReportFormat)
        {
            JasperExecutionRequest request = new JasperExecutionRequest();

            request.reportUnitUri = FileUrl;
            request.async = true;
            request.freshData = false;
            request.saveDataSnapshot = false;
            request.outputFormat = JasperReportFormat.ToString();
            request.interactive = false;
            request.ignorePagination = true;

            return GetAsync(FileUrl, Param, JasperReportFormat.PDF, request);
        }

        [Obsolete("Nao usar", true)]
        private JasperResultAsync GetAsync(string FileUrl, object Param, JasperReportFormat JasperReportFormat, JasperExecutionRequest RequestParam)
        {
            WebClient web = new WebClient();
            web.Credentials = new NetworkCredential(Auth.GetUsuario(), Auth.GetPassword());
            web.Headers.Add("Content-Type", "application/json");

            string Endereco = string.Format("{0}reportExecutions", Config.GetBaseUrl());

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

        public JasperResult Folder(string Path, JasperReportFolderAction Action, JasperDescriptor Descriptor)
        {
            JasperHttpMethod method = JasperHttpMethod.GET;

            switch (Action)
            {
                case JasperReportFolderAction.CREATE:
                    {
                        method = JasperHttpMethod.POST;
                        Descriptor.CreationDate = DateTime.Now;
                    }
                    break;
                case JasperReportFolderAction.DELETE:
                    {
                        method = JasperHttpMethod.DELETE;
                    }
                    break;
                case JasperReportFolderAction.UPDATE:
                    {
                        method = JasperHttpMethod.POST;
                        Descriptor.UpdateDate = DateTime.Now;
                    }
                    break;
                default:
                    break;
            }

            WebClient web = new WebClient();
            web.Credentials = new NetworkCredential(Auth.GetUsuario(), Auth.GetPassword());
            web.Headers.Add("Content-Type", " application/repository.folder+json");

            string url = string.Format("{0}resources{1}", Config.GetBaseUrl(), Path);

            try
            {
                string response = web.UploadString(url, method.ToString(), Descriptor.ToJson());

                return new JasperResult(url, null, JasperStatus.Success, response);
            }
            catch (WebException ex)
            {
                return new JasperResult(url, Descriptor, JasperStatus.Error, ex.Message);
            }
        }

        public JasperResult Folder(string Path, JasperReportFolderAction Action)
        {
            return Folder(Path, Action, new JasperDescriptor());
        }

        public JasperResult File(string Path, JasperReportFileAction Action, JasperDescriptor Descriptor)
        {
            JasperHttpMethod method = JasperHttpMethod.POST;

            switch (Action)
            {
                case JasperReportFileAction.UPLOAD:
                    {
                        method = JasperHttpMethod.POST;
                        Descriptor.CreationDate = DateTime.Now;
                    }
                    break;
                case JasperReportFileAction.DELETE:
                    {
                        method = JasperHttpMethod.DELETE;
                    }
                    break;
                default:
                    break;
            }

            WebClient web = new WebClient();
            web.Credentials = new NetworkCredential(Auth.GetUsuario(), Auth.GetPassword());
            web.Headers.Add("Content-Type", "application/repository.file+json");

            string url = string.Format("{0}resources{1}", Config.GetBaseUrl(), Path);

            try
            {
                string response = web.UploadString(url, method.ToString(), Descriptor.ToJson());

                return new JasperResult(url, null, JasperStatus.Success, response);
            }
            catch (WebException ex)
            {
                return new JasperResult(url, Descriptor, JasperStatus.Error, ex.Message);
            }
        }

        public JasperResult File(string Path, JasperReportFileAction Action)
        {
            return File(Path, Action, new JasperDescriptor());
        }
    }
}
