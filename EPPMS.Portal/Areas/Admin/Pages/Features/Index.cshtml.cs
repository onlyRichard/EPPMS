using EPPMS.Application.DTOs.Feature;
using EPPMS.Application.Interfaces.Services;
using EPPMS.Portal.Exceptions;
using EPPMS.Portal.Services.Interfaces;
using EPPMS.Portal.ViewModels.Feature;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Features;

public class IndexModel : PageModel
{
    private readonly IFeatureService _featureService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IFeatureService featureService, ILogger<IndexModel> logger)
    {
        ArgumentNullException.ThrowIfNull(featureService);
        ArgumentNullException.ThrowIfNull(logger);
        _featureService = featureService;
        _logger = logger;
    }

    public List<FeatureDetailsDTO> Features { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        try
        {
            Features = await _featureService.GetFeaturesAsync();
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "Failed to load features.");

            TempData["Error"] = ex.Message;

            Features = [];
        }
    }
}