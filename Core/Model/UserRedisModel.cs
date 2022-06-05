using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class UserRedisModel
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public long? BitwiseId { get; set; }
        public int? DealarId { get; set; }
        public string DealarName { get; set; }        
        public DateTime? CreatedDate { get; set; }
    }
}
