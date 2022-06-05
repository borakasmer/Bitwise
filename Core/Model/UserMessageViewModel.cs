using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class UserMessageViewModel
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string DealerName { get; set; }
    }
}
