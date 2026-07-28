namespace EPPMS.Portal.ViewModels.Feature;

public sealed class FeatureListViewModel
{
    public Guid FeatureId { get; set; }
    public Guid AppId { get; set; }
    public string ApplicationModule { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int RequestTypeId { get; set; }
    public string RequestType { get; set; } = string.Empty;
    public int PriorityId { get; set; }
    public string Priority { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DateRaised { get; set; }
    public DateTime? TargetRelease { get; set; }
    public int UsersImpacted { get; set; }
    public decimal ApproxEffort { get; set; }
    public bool IsActive { get; set; }
    public int ComplexityId { get; set; }
    public string Complexity { get; set; } = string.Empty;
}