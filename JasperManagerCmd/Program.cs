using JasperManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperManagerCmd
{
    class Program
    {
        static JasperClient client;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("JasperManager command line");
            Console.WriteLine("---------------------------------------------------");


            JasperAuthorization auth = new JasperAuthorization("jasperadmin", "jasperadmin");
            JasperConfig config = new JasperConfig("localhost");
            client = new JasperClient(config, auth);

            string command = args[0].ToLower();
            string origem = args[1];
            string destination = string.Empty;
            string dataSourcePath = string.Empty;

            if (args.Length >= 3)
                destination = args[2];

            if (args.Length >= 4)
                dataSourcePath = args[3];

            switch (command)
            {
                case "upload":
                    {
                        Upload(origem, destination);
                    } break;
                case "deploy":
                    {
                        Deploy(origem, destination, dataSourcePath);
                    } break;
                case "clear":
                    {
                        Clear(origem);
                    } break;
                default:
                    break;
            }

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Finish");

        }

        private static void Clear(string origem)
        {
            if (origem.Length < 7)
            {
                Console.WriteLine("Danger command aborted.");
                return;
            }

            var resultado = client.Folder(origem, JasperReportFolderAction.DELETE);

            Console.WriteLine("Deleting folder \"{1}\" => {0}", resultado.GetStatus().ToString(), origem);
        }

        private static void Deploy(string origem, string destination, string dataSourcePath)
        {
            string[] files = System.IO.Directory.GetFiles(origem);

            foreach (var file in files)
            {
                FileInfo info = new FileInfo(file);

                if (info.Extension != ".jrxml")
                    continue;

                string OnlyName = info.Name.Replace(info.Extension, "");

                Console.Write("Sending jrxml file: {0}", OnlyName);

                JasperDescriptor descriptor = new JasperDescriptor();
                descriptor.Label = OnlyName;
                descriptor.Description = OnlyName;
                descriptor.PermissionMark = 0;
                descriptor.Version = 0;
                descriptor.Type = info.Extension.Replace(".", "");
                descriptor.ContentFile(file);

                var resultado = client.File(string.Format("{0}/jrxml", destination), JasperReportFileAction.UPLOAD, descriptor);
                Console.Write(" => {0}", resultado.GetStatus().ToString());
                Console.WriteLine();

                if (resultado.GetStatus() == JasperStatus.Success)
                {
                    Console.Write("Creating the report file for \"{0}\"", OnlyName);
                    string FileNameReportJrxml = ValidateFileName(OnlyName);
                    JasperDescriptor descriptorDeploy = new JasperDescriptor();

                    descriptorDeploy.Label = info.Name;
                    descriptorDeploy.AlwaysPromptControls = true;
                    descriptorDeploy.ControlsLayout = ControlsLayout.inPage.ToString();
                    descriptorDeploy.DataSource = new JasperCollection<JasperDataSourceReference> { new JasperDataSourceReference(dataSourcePath) };
                    descriptorDeploy.Jrxml = new JasperCollection<JrxmlFileReference> { new JrxmlFileReference(string.Format("{0}/jrxml/{1}", destination, FileNameReportJrxml)) };

                    var resultadoDeploy = client.DeployReport(destination, descriptorDeploy);
                    Console.Write(" => {0}", resultadoDeploy.GetStatus().ToString());
                    Console.WriteLine();
                }

                Console.WriteLine();
            }

        }

        public static void Upload(string origem, string destination)
        {
            string[] files = System.IO.Directory.GetFiles(origem);

            foreach (var file in files)
            {
                FileInfo info = new FileInfo(file);
                Console.Write("Name: {0} || Extension: {1}", info.Name, info.Extension);

                JasperDescriptor descriptor = new JasperDescriptor();
                descriptor.Label = info.Name;
                descriptor.Description = info.Name;
                descriptor.PermissionMark = 0;
                descriptor.Version = 0;
                descriptor.Type = info.Extension.Replace(".", "");
                descriptor.ContentFile(file);

                var resultado = client.File(destination, JasperReportFileAction.UPLOAD, descriptor);
                Console.Write(" => {0}", resultado.GetStatus().ToString());
                Console.WriteLine();
            }
        }

        private static string ValidateFileName(string fileName)
        {
            return fileName.Replace(" ", "_").Replace(")", "_").Replace("(", "_");
        }
    }
}
