using Core.Caching;
using Core.Model;
using DB.Entities;
using DB.Entities.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BitwiseService
{
    public class UserService : IUserService
    {
        BitwiseContext _context;
        private readonly IRedisCacheService _redisCacheManager;
        public UserService(BitwiseContext context, IRedisCacheService redisCacheManager)
        {
            _context = context;
            _redisCacheManager = redisCacheManager;
        }
        /*public List<UserMessageViewModel> GetUserListByMessageID(int messageID)
        {
            var data = (from um in _context.UserMessage
                        join u in _context.User on um.UserId equals u.Id into us
                        from u in us.DefaultIfEmpty()
                        join d in _context.Dealar on um.DealerId equals d.Id into de
                        from d in de.DefaultIfEmpty()
                        join m in _context.Message on um.MessageId equals m.Id into me
                        from m in me.DefaultIfEmpty()
                        where um.MessageId == messageID
                        select new UserMessageViewModel
                        {
                            UserName = u.Name,
                            UserSurname = u.Surname,
                            DealerName = d.Name,
                            Message = m.Text,
                            IsRead = (bool)um.IsRead
                        }).ToList();
            return data.Count > 0 ? data : new List<UserMessageViewModel>();

        }*/
        public List<UserMessageViewModel> GetUserListByMessageID(int messageID)
        {
            var answerData = _context.UserMessageBitwise.Where(umb => 
            umb.MessageId == messageID && umb.IsDeleted == false).ToList();

            string message = _context.Message.FirstOrDefault(m => m.Id == messageID).Text;

            List<UserMessageViewModel> resultUserList = new();
            foreach (var answer in answerData)
            {
                int dealerID = answer.DealerId;                
                string cacheKeyGetAllUsers = $"Users:{dealerID}";
                var getAllUsersResult = _redisCacheManager.Get<List<UserRedisModel>>(cacheKeyGetAllUsers);
                if (getAllUsersResult == null)
                {
                    getAllUsersResult = _context.User
                         .Join(_context.Dealar,
                         user => user.DealarId,
                         dealer => dealer.Id,
                         (user, dealer) => new { user, dealer })
                        .Where(u => u.user.DealarId == dealerID && u.user.IsDeleted == false)
                        .Select(u => new UserRedisModel()
                        {
                            Name = u.user.Name,
                            Surname = u.user.Surname,
                            DealarId = u.user.DealarId,
                            DealarName = u.dealer.Name,
                            Id = u.user.Id,
                            BitwiseId = u.user.BitwiseId,
                            CreatedDate = DateTime.Now
                        })
                        .ToList();
                    _redisCacheManager.Set(cacheKeyGetAllUsers, getAllUsersResult);
                }
                foreach (var user in getAllUsersResult)
                {
                    if (user.BitwiseId == (answer.TotalUserBitwiseId & user.BitwiseId))
                    {
                        resultUserList.Add(new UserMessageViewModel
                        {
                            UserName = user.Name,
                            UserSurname = user.Surname,
                            DealerName = user.DealarName,
                            IsRead = (bool)answer.IsRead,
                            Message = message
                        });
                    }
                }
            }
            return resultUserList;
        }
    }
}