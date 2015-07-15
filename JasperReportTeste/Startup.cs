using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JasperReportTeste.Startup))]
namespace JasperReportTeste
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
