using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.Lookup
{
    public class LookupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int SortOrder { get; set; }
    }
}
