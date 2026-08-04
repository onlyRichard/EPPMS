using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.Task
{
    public class TaskDetailsDTO
    {
        public string TaskId { get; set; }
        public string FeatureId { get; set; }
        public string FeatureTitle { get; set; } = string.Empty;
        public string TechModuleId { get; set; }
        public string TechModule { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int PriorityId { get; set; }
        public string Priority { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string EstimatedStartDate { get; set; }
        public string EstimatedEndDate { get; set; }
        public string ActualStartDate { get; set; }
        public string ActualEndDate { get; set; }
        public string? LatestUpdate { get; set; }
        public string? Notes { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public string UpdatedDateTime { get; set; }
        public string IsActive { get; set; }
        public string BugId { get; set; }
        public string BugTitle { get; set; }
    }
}
