using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.BLL.DTO
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime CreateDate { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
