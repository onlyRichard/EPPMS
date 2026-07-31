namespace EPPMS.Portal.ViewModels.Application;

public sealed class ApplicationIndexViewModel
{
    public List<ApplicationListViewModel> Applications { get; set; } = [];

    public string? Search { get; set; }

    public int? CurrentHealthId { get; set; }

    public bool IsActive { get; set; } = true;
}