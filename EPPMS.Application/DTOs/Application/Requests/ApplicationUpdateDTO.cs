using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.Application.Requests
{
    public class ApplicationUpdateDTO
    {
        public Guid AppId { get; set; }
        public string ApplicationModule { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public string TechDetails { get; set; } = string.Empty;
        public string ProductionUrl { get; set; } = string.Empty;
        public int CurrentHealthId { get; set; }
        public string? Notes { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
