using EPPMS.Portal.Exceptions;
using EPPMS.Portal.Services.Interfaces;
using EPPMS.Portal.ViewModels.Feature;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Features;

public class IndexModel : PageModel
{
    private readonly IFeatureApiClient _featureApiClient;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IFeatureApiClient featureApiClient, ILogger<IndexModel> logger)
    {
        ArgumentNullException.ThrowIfNull(featureApiClient);
        ArgumentNullException.ThrowIfNull(logger);
        _featureApiClient = featureApiClient;
        _logger = logger;
    }

    public List<FeatureListViewModel> Features { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        try
        {
            Features = await _featureApiClient.GetFeaturesAsync(cancellationToken: cancellationToken);
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "Failed to load features.");

            TempData["Error"] = ex.Message;

            Features = [];
        }
    }
}