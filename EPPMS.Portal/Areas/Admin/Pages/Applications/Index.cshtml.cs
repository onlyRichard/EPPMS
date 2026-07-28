using EPPMS.Application.DTOs.Application.Response;
using EPPMS.Application.Interfaces.Services;
using EPPMS.Portal.Exceptions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Applications;

public class IndexModel : PageModel
{
    private readonly IApplicationService _applicationService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IApplicationService applicationService, ILogger<IndexModel> logger)
    {
        ArgumentNullException.ThrowIfNull(applicationService);
        ArgumentNullException.ThrowIfNull(logger);
        _applicationService = applicationService;
        _logger = logger;
    }

    public List<ApplicationListResponseDTO> Applications { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        try
        {
            Applications = await _applicationService.GetApplicationsAsync();
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "Failed to load applications.");

            TempData["Error"] = ex.Message;

            Applications = [];
        }
    }
}