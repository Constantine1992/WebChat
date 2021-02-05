using ChatWeb.DAL.EF;
using ChatWeb.DAL.IRepository;
using ChatWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.DAL.Repository
{
    public class ChatRepository : IChatRepository
    {
        private ChatContext context;
        public ChatRepository(ChatContext context)
        {
            this.context = context;     
        }

        public void Create(Chat item)
        {
            context.Chats.Add(item);
        }

        public void Delete(int id)
        {
            Chat chat = GetById(id);
            if (chat != null)
                context.Chats.Remove(chat);
        }

        public IEnumerable<Chat> Find(Expression<Func<Chat, bool>> predicate)
        {
            return context.Chats.Where(predicate);
        }
        

        public Chat GetById(int id)
        {
            return context.Chats.Find(id);
        }

        public void Update(Chat item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
        
    }
}
