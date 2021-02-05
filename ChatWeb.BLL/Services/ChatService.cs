using ChatWeb.BLL.DTO;
using ChatWeb.BLL.IServisces;
using ChatWeb.DAL.IRepository;
using ChatWeb.DAL.IUserRepositories;
using ChatWeb.DAL.Models;
using FilterProvider;
using FilterProvider.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.BLL.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWorkChat db;

        public ChatService(IUnitOfWorkChat db)
        {
            this.db = db;    
        }
        public IEnumerable<ChatDTO> GetChats(FilterChatDTO filter)
        {
            OrElseBuilder<Chat> builder = OrElseBuilder<Chat>.CreateBuilder();
            builder.AppendAndAlso(n => n.From == filter.User || n.To == filter.User);
            builder.AppendAndAlso(n => !string.IsNullOrEmpty(n.Message));
            builder.AppendAndAlso(n => n.CreateDate >= filter.BeginDate, filter.BeginDate != DateTime.MinValue);
            builder.AppendAndAlso(n => n.CreateDate <= filter.EndDate, filter.EndDate != DateTime.MinValue);

            List<ChatDTO> chats = new List<ChatDTO>();
            foreach (var chat in db.ChatRepository.Find(builder.GetFilter()).OrderBy(n=>n.CreateDate))
                chats.Add(Map(chat));
            return chats;
        }

        public void SaveChat(ChatDTO chatDTO)
        {
            var chat = Map(chatDTO);
            db.ChatRepository.Create(chat);
            db.Save();
        }

        public void SaveChats(IEnumerable<ChatDTO> chatsDTO)
        {
            foreach (var chatDTO in chatsDTO)
            {
                SaveChat(chatDTO);
            }
        }
        private Chat Map(ChatDTO chatDTO)
        {
            Chat chat = new Chat
            {
                CreateDate = chatDTO.CreateDate,
                From = chatDTO.From,
                To = chatDTO.To,
                IsRead = chatDTO.IsRead,
                Message = chatDTO.Message
            };
            return chat;
        }
        private ChatDTO Map(Chat chatDTO)
        {
            ChatDTO chat = new ChatDTO
            {
                CreateDate = chatDTO.CreateDate,
                From = chatDTO.From,
                To = chatDTO.To,
                IsRead = chatDTO.IsRead,
                Message = chatDTO.Message
            };
            return chat;
        }
    }
}
