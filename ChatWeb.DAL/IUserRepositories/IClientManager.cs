using ChatWeb.DAL.AutorisationUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.DAL.IUserRepositories
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
