using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class Dealar
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CityName { get; set; }
        public string? TaxNumber { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
