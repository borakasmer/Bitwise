using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class UserMessageBitwise
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
        public int MessageId { get; set; }
        public long TotalUserBitwiseId { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
