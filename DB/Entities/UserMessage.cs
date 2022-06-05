using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class UserMessage
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
