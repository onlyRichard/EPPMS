using EPPMS.Portal.Exceptions;
using EPPMS.Portal.Services.Interfaces;
using EPPMS.Portal.ViewModels.Application;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Applications;

public class IndexModel : PageModel
{
    private readonly IApplicationApiClient _applicationApiClient;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(
        IApplicationApiClient applicationApiClient,
        ILogger<IndexModel> logger)
    {
        ArgumentNullException.ThrowIfNull(applicationApiClient);
        ArgumentNullException.ThrowIfNull(logger);

        _applicationApiClient = applicationApiClient;
        _logger = logger;
    }

    public List<ApplicationListViewModel> Applications { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        try
        {
            Applications = await _applicationApiClient.GetApplicationsAsync(
                cancellationToken: cancellationToken);
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "Failed to load applications.");

            TempData["Error"] = ex.Message;

            Applications = [];
        }
    }
}