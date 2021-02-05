using ChatWeb.BLL.IServisces;
using ChatWeb.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatWeb.UserHub
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        private readonly IChatService service;
        public ChatHub(IChatService service)
        {
            this.service = service;
        }
        static List<User> Users = new List<User>();
        public void Send(string from, string to, string message)
        {
            Clients.All.addMessage(from, message);
            service.SaveChat(new BLL.DTO.ChatDTO
            {
                CreateDate = DateTime.Now,
                From = from, To = to, Message = message
            });

        }
        public string GetUserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }
        public IEnumerable<BLL.DTO.ChatDTO> GetChats(string user, string userTo, string dateFrom, string dateTo)
        {
            DateTime from;
            DateTime to;
            DateTime.TryParse(dateFrom, out from);
            DateTime.TryParse(dateFrom, out to);
            return service.GetChats(new BLL.DTO.FilterChatDTO 
            { 
                User = user, UserTo = userTo,
                BeginDate = from,
                EndDate = to
            });
        }
       
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;           
            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, Name = userName });
                Clients.Caller.onConnected(id, userName, Users);
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}