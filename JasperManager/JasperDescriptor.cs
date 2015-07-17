using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperManager
{
    public class JasperDescriptor
    {
        public void ContentFile(byte[] Data)
        {
            string Base64Text = Convert.ToBase64String(Data);

            this.Content = Base64Text;
        }

        public string Label { get; set; }

        public string Description { get; set; }

        public int? PermissionMark { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? Version { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }

        public bool? AlwaysPromptControls { get; set; }

        public string ControlsLayout { get; set; }

        public string InputControlRenderingView { get; set; }

        public string ReportRenderingView { get; set; }

        public object DataSource { get; set; }

        public object Query { get; set; }

        public object Jrxml { get; set; }

        public object InputControls { get; set; }

        public object Resources { get; set; }
    }
}
