using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArticleViewerWebApplication.Startup))]
namespace ArticleViewerWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
