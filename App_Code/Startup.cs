using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EBookDownloader.Startup))]
namespace EBookDownloader
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
