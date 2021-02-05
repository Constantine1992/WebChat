using ChatWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.DAL.IRepository
{
    public interface IChatRepository: IBaseRepository<Chat>
    {
    }
}
