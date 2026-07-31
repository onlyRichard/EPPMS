namespace EPPMS.Portal.ViewModels.Application;

public sealed class ApplicationDeleteViewModel
{
    public Guid AppId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}