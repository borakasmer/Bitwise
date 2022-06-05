using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitwiseService
{
    public interface IUserService
    {
        List<UserMessageViewModel> GetUserListByMessageID(int messageID);
    }
}
