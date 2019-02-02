using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zeka.Startup))]
namespace Zeka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
