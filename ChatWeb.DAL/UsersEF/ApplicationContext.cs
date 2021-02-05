using ChatWeb.DAL.AutorisationUsers;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.DAL.UsersEF
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string connectionString)
            :base(connectionString)
        {
            
        }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
