using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.Feature
{
    public class FeatureCreateDTO
    {
        public Guid FeatureId { get; set; }
        public Guid AppId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int RequestTypeId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string RequestedBy { get; set; } = string.Empty;
        public DateTime? DateRaised { get; set; }
        public string BusinessNeed { get; set; } = string.Empty;
        public string ExpectedValue { get; set; } = string.Empty;
        public int PriorityId { get; set; }
        public int UsersImpacted { get; set; }
        public string InitialTechAssessment { get; set; } = string.Empty;
        public int ComplexityId { get; set; }
        public decimal ApproxEffort { get; set; }
        public int StatusId { get; set; }
        public DateTime? TargetRelease { get; set; }
        public string? LinksNotes { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
