using ChatWeb.DAL.IRepository;
using ChatWeb.DAL.Repository;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.BLL.Services
{
    public class ChatModule : NinjectModule
    {
        private string connectionString;
        public ChatModule(string connectionString)
        {
            this.connectionString = connectionString;    
        }
        public override void Load()
        {
            Bind<IUnitOfWorkChat>().To<UnitOfWorkChat>().InSingletonScope().WithConstructorArgument(connectionString);
        }
    }
}
