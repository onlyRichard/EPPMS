namespace EPPMS.Portal.ViewModels.Application;

public sealed class ApplicationDetailsViewModel
{
    public Guid AppId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string TechDetails { get; set; } = string.Empty;
    public string? ProductionUrl { get; set; }
    public int CurrentHealthId { get; set; }
    public string CurrentHealth { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}