using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.Common
{
    public class LookUpItemDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public int SortOrder { get; init; }
    }
}
