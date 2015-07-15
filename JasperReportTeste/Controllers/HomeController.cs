using JasperManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace JasperReportTeste.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();

            //WebClient web = new WebClient();

            //web.Credentials = new NetworkCredential("jasperadmin", "jasperadmin");
            ////web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            //var arquivo = web.DownloadData("http://localhost:8080/jasperserver/rest_v2/reports/reports/interactive/CustomersReport.pdf");

            //return File(arquivo, "application/pdf");
        }

        public ActionResult GetReport()
        {
            JasperConfig config = new JasperConfig("labvm-spfvm2", 8080, "jasperserver");
            JasperAuthorization auth = new JasperAuthorization("jasperadmin", "jasperadmin");
            JasperClient report = new JasperClient(config, auth);

            var response = report.Get("/reports/Banco", new { UsuarioLogado = "Tadeu Rodrigues Arias" }, JasperReportFormat.PDF);

            return File(response.GetDocumento(), response.GetJasperContentType(), response.DefineFileName("Exemplo"));
        }

    }
}