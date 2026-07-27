using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.Task
{
    public class TaskDetailsDTO
    {
        public Guid TaskId { get; set; }
        public Guid FeatureId { get; set; }
        public string FeatureTitle { get; set; } = string.Empty;
        public Guid TechModuleId { get; set; }
        public string TechModule { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int PriorityId { get; set; }
        public string Priority { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? EstimatedStartDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string? LatestUpdate { get; set; }
        public string? Notes { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
