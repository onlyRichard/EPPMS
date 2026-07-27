namespace EPPMS.Portal.Helpers;

public sealed class ApiSettings
{  

    public const string SectionName = "ApiSettings";
    public string AdminBaseUrl { get; set; } = string.Empty;

    public string UserBaseUrl { get; set; } = string.Empty;
}