using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.TechnicalModule
{
    public class TechnicalModuleUpdateDTO
    {
        public Guid TechModuleId { get; set; }
        public string TechModule { get; set; } = string.Empty;
        public int TechnologyAreaId { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Reason { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public DateTime? PlannedStart { get; set; }
        public DateTime? TargetCompletion { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? ActualCompletion { get; set; }
        public decimal? EstimatedEffort { get; set; }
        public decimal? ActualEffort { get; set; }
        public string? ReleaseImpact { get; set; }
        public string? LatestUpdate { get; set; }
        public string? Notes { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
