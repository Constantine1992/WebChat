using ChatWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.DAL.EF
{
    public class ChatContext: DbContext
    {
        public ChatContext()
        {
            Database.SetInitializer(new DBInitializer());
        }
        public ChatContext(string connectionString)
            :base(connectionString)
        {
            var ensureDLLIsCopied =
                 System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public DbSet<Chat> Chats { get; set; }
    }
}
