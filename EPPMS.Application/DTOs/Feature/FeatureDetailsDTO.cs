using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.Feature
{
    public class FeatureDetailsDTO
    {
        public Guid FeatureId { get; set; }
        public Guid AppId { get; set; }
        public string ApplicationModule { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int RequestTypeId { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RequestedBy { get; set; } = string.Empty;
        public DateTime DateRaised { get; set; }
        public string BusinessNeed { get; set; } = string.Empty;
        public string ExpectedValue { get; set; } = string.Empty;
        public int PriorityId { get; set; }
        public string Priority { get; set; } = string.Empty;
        public int UsersImpacted { get; set; }
        public string InitialTechAssessment { get; set; } = string.Empty;
        public int ComplexityId { get; set; }
        public string Complexity { get; set; } = string.Empty;
        public decimal ApproxEffort { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? TargetRelease { get; set; }
        public string? LinksNotes { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
