using ChatWeb.BLL.IServisces;
using ChatWeb.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatWeb.Utils
{
    public class ChatWebModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IChatService>().To<ChatService>().InSingletonScope();
        }
    }
}