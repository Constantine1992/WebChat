using ChatWeb.DAL.EF;
using ChatWeb.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.DAL.Repository
{
    public class UnitOfWorkChat : IUnitOfWorkChat
    {
        ChatContext chatContext;
        IChatRepository repository;
        public UnitOfWorkChat(string connectionString)
        {
            chatContext = new ChatContext(connectionString);
        }
        public IChatRepository ChatRepository
        {
            get
            {
                if (repository == null)
                    repository = new ChatRepository(chatContext);
                return repository;
            }
        }
        public void Save()
        {
            chatContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    chatContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
