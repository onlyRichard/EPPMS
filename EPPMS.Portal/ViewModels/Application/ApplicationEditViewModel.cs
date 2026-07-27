using System.ComponentModel.DataAnnotations;

namespace EPPMS.Portal.ViewModels.Application;

public sealed class ApplicationEditViewModel
{
    [Required]
    public Guid AppId { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    public int CurrentHealthId { get; set; }

    public bool IsActive { get; set; }

    public string ModifiedBy { get; set; } = string.Empty;
}