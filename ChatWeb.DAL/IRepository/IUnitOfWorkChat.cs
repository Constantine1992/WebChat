using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.DAL.IRepository
{
    public interface IUnitOfWorkChat
    {
        IChatRepository ChatRepository { get; }
        void Save();
    }
}
