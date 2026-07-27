namespace EPPMS.Portal.ViewModels.Application;

public sealed class ApplicationListViewModel
{
    public Guid AppId { get; set; }
    public string ApplicationModule { get; set; } = string.Empty;
    public string? Purpose { get; set; }
    public string TechDetails { get; set; } = string.Empty;
    public string CurrentHealth { get; set; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime UpdatedDate { get; set; }
    public bool IsActive { get; set; }
}