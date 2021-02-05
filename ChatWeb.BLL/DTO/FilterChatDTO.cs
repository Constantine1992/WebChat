using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.BLL.DTO
{
    public class FilterChatDTO
    {
        public string User { get; set; }
        public string UserFrom { get; set; }
        public string UserTo { get; set; }
       
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
