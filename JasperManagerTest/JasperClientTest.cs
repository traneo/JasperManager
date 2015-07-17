using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JasperManager;
using System.IO;

namespace JasperManagerTest
{
    [TestClass]
    public class JasperClientTest
    {
        public JasperClient GetClient()
        {
            JasperConfig config = new JasperConfig("localhost");
            JasperAuthorization auth = new JasperAuthorization("jasperadmin", "jasperadmin");
            JasperClient report = new JasperClient(config, auth);

            return report;
        }

        [TestMethod]
        public void GetCorretResponse()
        {
            JasperClient report = GetClient();

            var response = report.Get("/reports/interactive/TableReport", new { UsuarioLogado = "Usuario de Testes" }, JasperReportFormat.PDF);

            Assert.AreNotEqual(new byte[0], response.GetDocument());
            Assert.AreEqual("application/PDF", response.GetContentType());
            Assert.AreEqual("Exemplo.pdf", response.DefineFileName("Exemplo"));
        }

        [TestMethod]
        public void MakeFolder()
        {
            JasperClient report = GetClient();

            JasperDescriptor descriptor = new JasperDescriptor();

            descriptor.Label = "Relatorios de Exemplo";
            descriptor.Description = "Pasta com exemplos";
            descriptor.PermissionMark = 0;
            descriptor.Version = 0;

            JasperResult result = report.Folder("/reports/teste", JasperReportFolderAction.CREATE, descriptor);

            Assert.AreEqual(result.GetStatus(), JasperStatus.Success);
        }

        [TestMethod]
        public void UploadFile()
        {
            JasperClient report = GetClient();

            byte[] reportFile = null;
            using (StreamReader read = new StreamReader(@"..\..\Files\Exemplo.jrxml"))
            {
                reportFile = new byte[read.BaseStream.Length];
                read.BaseStream.Read(reportFile, 0, (int)read.BaseStream.Length);
            }

            JasperDescriptor descriptor = new JasperDescriptor();

            descriptor.Label = "Arquivo jasper em xml";
            descriptor.Description = "arquivo de teste";
            descriptor.PermissionMark = 0;
            descriptor.Version = 0;
            descriptor.Type = "jrxml";
            descriptor.ContentFile(reportFile);

            JasperResult result = report.File("/reports/teste/Relatorios_de_Exemplo", JasperReportFileAction.UPLOAD, descriptor);

            Assert.AreEqual(result.GetStatus(), JasperStatus.Success, result.GetMessage());
        }

        [TestMethod]
        public void DeleteFile()
        {
            JasperClient report = GetClient();

            JasperResult result = report.File("/reports/teste/Relatorios_de_Exemplo/Arquivo_jasper_em_xml", JasperReportFileAction.DELETE);

            Assert.AreEqual(result.GetStatus(), JasperStatus.Success, result.GetMessage());
        }

        [TestMethod]
        public void DeleteFolder()
        {
            JasperClient report = GetClient();

            JasperResult result = report.Folder("/reports/teste", JasperReportFolderAction.DELETE);

            Assert.AreEqual(result.GetStatus(), JasperStatus.Success, result.GetMessage());
        }

    }
}
