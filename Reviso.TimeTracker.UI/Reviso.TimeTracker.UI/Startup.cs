using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Reviso.TimeTracker.UI.Startup))]
namespace Reviso.TimeTracker.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
