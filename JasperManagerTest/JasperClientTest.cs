using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JasperManager;
using System.IO;
using System.Collections.Generic;

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

            var response = report.Get("/reports/interactive/TableReport", new { UsuarioLogado = "User" }, JasperReportFormat.PDF);

            Assert.AreNotEqual(new byte[0], response.GetDocument());
            Assert.AreEqual("application/PDF", response.GetContentType());
            Assert.AreEqual("Exemplo.pdf", response.DefineFileName("Exemplo"));
        }

        [TestMethod]
        public void MakeFolder()
        {
            JasperClient report = GetClient();

            JasperDescriptor descriptor = new JasperDescriptor();

            descriptor.Label = "ReportFile";
            descriptor.Description = "Folder Sample";
            descriptor.PermissionMark = 0;
            descriptor.Version = 0;

            JasperResult result = report.Folder("/reports", JasperReportFolderAction.CREATE, descriptor);

            Assert.AreEqual(result.GetStatus(), JasperStatus.Success);
        }

        [TestMethod]
        public void UploadJrxmlFile()
        {
            JasperClient report = GetClient();

            byte[] reportFile = null;
            using (StreamReader read = new StreamReader(@"..\..\..\JasperManagerTest\Files\Exemplo.jrxml"))
            {
                reportFile = new byte[read.BaseStream.Length];
                read.BaseStream.Read(reportFile, 0, (int)read.BaseStream.Length);
            }

            JasperDescriptor descriptor = new JasperDescriptor();

            descriptor.Label = "Sample";
            descriptor.Description = "Test File";
            descriptor.PermissionMark = 0;
            descriptor.Version = 0;
            descriptor.Type = "jrxml";
            descriptor.ContentFile(reportFile);

            JasperResult result = report.File("/reports/jrxml", JasperReportFileAction.UPLOAD, descriptor);

            Assert.AreEqual(result.GetStatus(), JasperStatus.Success, result.GetMessage());
        }

        [TestMethod]
        public void DeployReportAsNewDocument()
        {
            JasperDescriptor descriptor = new JasperDescriptor();

            descriptor.Label = "SampleReport";
            descriptor.AlwaysPromptControls = false;
            descriptor.ControlsLayout = ControlsLayout.inPage.ToString();
            descriptor.DataSource = new JasperCollection<JasperDataSourceReference> { new JasperDataSourceReference("/datasources/repositoryDS") };
            descriptor.Jrxml = new JasperCollection<JrxmlFileReference> { new JrxmlFileReference("/reports/jrxml/Sample") };

            JasperClient report = GetClient();

            JasperResult result = report.DeployReport("/reports/ReportFile/Test", descriptor);

            Assert.AreEqual(result.GetStatus(), JasperStatus.Success, result.GetMessage());
        }

        [TestMethod]
        public void GetNewDocument()
        {
            JasperClient report = GetClient();

            var response = report.Get("/reports/ReportFile/Test/SampleReport", new { UsuarioLogado = "User" }, JasperReportFormat.PDF);

            Assert.AreNotEqual(new byte[0], response.GetDocument());
            Assert.AreEqual("application/PDF", response.GetContentType());
            Assert.AreEqual("Exemplo.pdf", response.DefineFileName("Exemplo"));
        }

        [TestMethod]
        public void DeleteReport()
        {
            JasperClient report = GetClient();

            JasperResult result = report.File("/reports/ReportFile/Test/SampleReport", JasperReportFileAction.DELETE);

            Assert.AreEqual(result.GetStatus(), JasperStatus.Success, result.GetMessage());
        }

        [TestMethod]
        public void DeleteFolderJrxml()
        {
            JasperClient report = GetClient();

            JasperResult result = report.Folder("/reports/jrxml", JasperReportFolderAction.DELETE);

            Assert.AreEqual(result.GetStatus(), JasperStatus.Success, result.GetMessage());
        }

        [TestMethod]
        public void DeleteFolderReportFile()
        {
            JasperClient report = GetClient();

            JasperResult result = report.Folder("/reports/ReportFile", JasperReportFolderAction.DELETE);

            Assert.AreEqual(result.GetStatus(), JasperStatus.Success, result.GetMessage());
        }

    }
}
