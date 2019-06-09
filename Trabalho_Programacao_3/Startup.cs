using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trabalho_Programacao_3.Startup))]
namespace Trabalho_Programacao_3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}