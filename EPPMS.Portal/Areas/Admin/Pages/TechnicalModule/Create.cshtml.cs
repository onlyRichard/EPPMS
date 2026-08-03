using EPPMS.Application.DTOs.Feature;
using EPPMS.Application.DTOs.Lookup;
using EPPMS.Application.DTOs.TechnicalModule;
using EPPMS.Application.Interfaces.Services;
using EPPMS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.TechnicalModule
{
    public class CreateModel : PageModel
    {
        private readonly ILookupService _lookupService;
        private readonly ITechnicalModuleService _technicalmoduleService;
        public CreateModel(ILookupService lookupService, ITechnicalModuleService technicalmoduleService)
        {
            ArgumentNullException.ThrowIfNull(lookupService);
            _lookupService = lookupService;
            _technicalmoduleService = technicalmoduleService;
        }
        [BindProperty]
        public TechnicalModuleCreateDTO TechnicalModule { get; set; } = new();
        public IReadOnlyList<LookupResponseDTO> RequestTypes { get; private set; } = [];
        public IReadOnlyList<LookupResponseDTO> Priorities { get; private set; } = [];
        public IReadOnlyList<LookupResponseDTO> Statuses { get; private set; } = [];
        public IReadOnlyList<LookupResponseDTO> TechnologyAreas { get; private set; } = [];

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            await LoadLookupsAsync(cancellationToken);
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                await LoadLookupsAsync(cancellationToken);
                return Page();
            }

            try
            {
                await _technicalmoduleService.CreateTechnicalModuleAsync(TechnicalModule);

                TempData["Success"] = "Application created successfully.";

                return RedirectToPage("./Index");
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.InnerException.Message;

                await LoadLookupsAsync(cancellationToken);

                return Page();
            }
        }

        private async Task LoadLookupsAsync(CancellationToken cancellationToken)
        {
            Priorities = await _lookupService.GetPrioritiesAsync(cancellationToken);
            Statuses = await _lookupService.GetStatusesAsync(cancellationToken);
            TechnologyAreas = await _lookupService.GetTechnologyAreasAsync(cancellationToken);
        }
    }
}
