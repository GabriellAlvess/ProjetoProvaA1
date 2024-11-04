using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetoProvaA1.Startup))]
namespace ProjetoProvaA1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }


}
