using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatWeb.Controllers
{
    [Authorize]
    public class ChatWebController : Controller
    {
        // GET: ChatWeb
        [Authorize()]
        public ActionResult Index()
        {
            return View();
        }
    }
}