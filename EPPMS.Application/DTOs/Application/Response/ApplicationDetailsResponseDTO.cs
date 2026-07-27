using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.Application.Response
{
    public sealed class ApplicationDetailsResponseDTO
    {
        public Guid AppId { get; set; }
        public string ApplicationModule { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public string TechDetails { get; set; } = string.Empty;
        public string ProductionUrl { get; set; } = string.Empty;
        public int CurrentHealthId { get; set; }
        public string CurrentHealth { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
