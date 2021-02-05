﻿using ChatWeb.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.BLL.IServisces
{
    public interface IChatService
    {
        void SaveChat(ChatDTO chatDTO);
        void SaveChats(IEnumerable<ChatDTO> chatsDTO);
        IEnumerable<ChatDTO> GetChats(FilterChatDTO filter);
    }
}
