using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.OngoingTask
{
    public class OngoingTaskDetailsDTO
    {
        public Guid OngoingTaskId { get; set; }
        public Guid AppId { get; set; }
        public string ApplicationModule { get; set; } = string.Empty;
        public int TypeId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int SeverityId { get; set; }
        public string Severity { get; set; } = string.Empty;
        public int PriorityId { get; set; }
        public string Priority { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CommentsUpdates { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
