using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public long? BitwiseId { get; set; }
        public int? DealarId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
