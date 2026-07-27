namespace EPPMS.Portal.Models.Navigation;

public sealed class NavigationItem
{
    public string Title { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public string Area { get; init; } = string.Empty;
    public string Route { get; init; } = string.Empty;
    public bool DividerBefore { get; init; }
}