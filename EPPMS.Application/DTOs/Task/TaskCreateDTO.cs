using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.Task
{
    public class TaskCreateDTO
    {
        public Guid TaskId { get; set; }
        public Guid FeatureId { get; set; }
        public Guid TechModuleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string? LatestUpdate { get; set; }
        public string? Notes { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
