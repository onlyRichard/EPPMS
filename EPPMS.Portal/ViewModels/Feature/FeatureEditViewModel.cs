using System.ComponentModel.DataAnnotations;

namespace EPPMS.Portal.ViewModels.Feature;

public sealed class FeatureEditViewModel
{
    [Required]
    public Guid FeatureId { get; set; }

    [Required]
    public Guid AppId { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int RequestTypeId { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(32)]
    public string RequestedBy { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateRaised { get; set; }

    [Required]
    public string BusinessNeed { get; set; } = string.Empty;

    [Required]
    public string ExpectedValue { get; set; } = string.Empty;

    [Required]
    public int PriorityId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int UsersImpacted { get; set; }

    public string? InitialTechAssessment { get; set; }

    [Required]
    public int ComplexityId { get; set; }

    [Required]
    public decimal ApproxEffort { get; set; }

    [Required]
    public int StatusId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? TargetRelease { get; set; }

    public string? LinksNotes { get; set; }
}