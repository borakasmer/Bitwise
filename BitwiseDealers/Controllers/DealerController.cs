using BitwiseService;
using Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace BitwiseDealers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealerController : ControllerBase
    {
        IUserService _userService;
        public DealerController(IUserService userService) { _userService = userService; }
        [HttpGet("GetUserListByMessageID/{messageID}")]
        public IEnumerable<UserMessageViewModel> GetUserListByMessageID(int messageID)
        {
            return _userService.GetUserListByMessageID(messageID);
        }
    }
}