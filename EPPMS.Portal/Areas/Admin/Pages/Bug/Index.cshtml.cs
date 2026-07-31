using EPPMS.Application.DTOs.Bug;
using EPPMS.Application.DTOs.Feature;
using EPPMS.Application.Interfaces.Services;
using EPPMS.Portal.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Bug
{
    public class IndexModel : PageModel
    {
        public List<BugDetailsDTO> Bug { get; private set; } = [];

        private readonly IBugService _bugService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IBugService bugService, ILogger<IndexModel> logger)
        {
            ArgumentNullException.ThrowIfNull(bugService);
            ArgumentNullException.ThrowIfNull(logger);
            _bugService = bugService;
            _logger = logger;
        }
        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            try
            {
                Bug = await _bugService.GetBugsAsync();
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "Failed to load features.");

                TempData["Error"] = ex.Message;

                Bug = [];
            }
        }
    }
}
