using EPPMS.Application.DTOs.Application.Response;
using EPPMS.Application.DTOs.TechnicalModule;
using EPPMS.Application.Interfaces.Services;
using EPPMS.Application.Services;
using EPPMS.Portal.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.TechnicalModule
{
    public class IndexModel : PageModel
    {
        private readonly ITechnicalModuleService _technicalmoduleService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ITechnicalModuleService technicalmoduleService, ILogger<IndexModel> logger)
        {
            ArgumentNullException.ThrowIfNull(technicalmoduleService);
            ArgumentNullException.ThrowIfNull(logger);
            _technicalmoduleService = technicalmoduleService;
            _logger = logger;
        }
        public List<TechnicalModuleDetailsDTO> TechnicalModule { get; private set; } = [];
        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            try
            {
                TechnicalModule = await _technicalmoduleService.GetTechnicalModulesAsync();
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "Failed to load features.");

                TempData["Error"] = ex.Message;

                TechnicalModule = [];
            }
        }
    }
}
