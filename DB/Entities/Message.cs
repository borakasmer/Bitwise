using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class Message
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
