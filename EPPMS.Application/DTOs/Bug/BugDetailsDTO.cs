using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.Bug
{
    public class BugDetailsDTO
    {
        public Guid BugId { get; set; }
        public Guid AppId { get; set; }
        public string ApplicationModule { get; set; } = string.Empty;
        public string BugTitle { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? NumberOfUsersImpacted { get; set; }
        public string ReportedBy { get; set; } = string.Empty;
        public DateTime ReportedDate { get; set; }
        public int SeverityId { get; set; }
        public string Severity { get; set; } = string.Empty;
        public int PriorityId { get; set; }
        public string Priority { get; set; } = string.Empty;
        public string? UserBusinessImpact { get; set; }
        public string? ReproductionSteps { get; set; }
        public string? RootCause { get; set; }
        public string? AssignedTo { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool WorkaroundAvailable { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int ReleaseStatusId { get; set; }
        public string ReleaseStatus { get; set; } = string.Empty;
        public decimal? EstimatedEffort { get; set; }
        public decimal? ActualEffort { get; set; }
        public int TestingStatusId { get; set; }
        public string TestingStatus { get; set; } = string.Empty;
        public DateTime? ProductionDeploymentDate { get; set; }
        public string? CommentsUpdates { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
