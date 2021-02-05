using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.DAL.AutorisationUsers
{
    public class ApplicationUser: IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
        private void Test()
        {
            ApplicationUser u = new ApplicationUser { Email = "" };
        }
    }
}
