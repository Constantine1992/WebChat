using ChatWeb.BLL.IServisces;
using ChatWeb.BLL.Services;
using ChatWeb.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Ninject.Modules;
using Owin;
[assembly: OwinStartup(typeof(ChatWeb.App_Start.Startup))]
namespace ChatWeb.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        private const string connection = "DefaultConnection";
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext( CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

            NinjectModule chatWebModule = new ChatWebModule();
            NinjectModule chatModule = new ChatModule(connection);
            var kernel = new StandardKernel(chatWebModule, chatModule);
            var resolver = new NinjectSignalRDependencyResolver(kernel);
           
            var config = new HubConfiguration();
            config.EnableDetailedErrors = true;
            config.EnableJavaScriptProxies = true;
            config.EnableJSONP = true;
            config.Resolver = resolver;

            app.MapSignalR(config);
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService(connection);
        }
    }
}