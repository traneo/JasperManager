using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JasperManager;

namespace JasperManagerTest
{
    [TestClass]
    public class JasperClientTest
    {
        [TestMethod]
        public void Get()
        {
            JasperConfig config = new JasperConfig("labvm-spfvm2");
            JasperAuthorization auth = new JasperAuthorization("jasperadmin", "jasperadmin");
            JasperClient report = new JasperClient(config, auth);

            JasperReportFormat wantedType = JasperReportFormat.PDF;
            string expected = string.Format("application/{0}", wantedType.ToString());
            string FileName = "Exemplo";
            string expectedFileName = string.Format("{0}.{1}", FileName, wantedType.ToString().ToLower());

            var response = report.Get("/reports/Banco", new { UsuarioLogado = "Usuario de Testes" }, wantedType);


            Assert.AreNotEqual(response.GetDocumento(), new byte[0]);
            Assert.AreEqual(expected, response.GetJasperContentType());
            Assert.AreEqual(expectedFileName, response.DefineFileName(FileName));

        }
    }
}
