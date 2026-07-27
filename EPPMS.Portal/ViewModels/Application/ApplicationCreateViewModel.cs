using System.ComponentModel.DataAnnotations;

namespace EPPMS.Portal.ViewModels.Application;

public sealed class ApplicationCreateViewModel
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    public int CurrentHealthId { get; set; }

    public bool IsActive { get; set; } = true;
}