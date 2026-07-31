using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.OngoingTask
{
    public class OngoingTaskCreateDTO
    {
        public Guid OngoingTaskId { get; set; }
        public Guid AppId { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int SeverityId { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CommentsUpdates { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
