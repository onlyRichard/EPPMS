using System.ComponentModel.DataAnnotations;

namespace EPPMS.Portal.ViewModels.Application;

public sealed class ApplicationCreateViewModel
{
    [Required]
    [StringLength(150)]
    public string ApplicationModule { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Purpose { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string TechDetails { get; set; } = string.Empty;

    [Url]
    [StringLength(500)]
    public string? ProductionUrl { get; set; }

    [Required]
    public int CurrentHealthId { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;
}