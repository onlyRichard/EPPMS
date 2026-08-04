using EPPMS.Application.DTOs.Feature;
using EPPMS.Application.DTOs.Lookup;
using EPPMS.Application.DTOs.Task;
using EPPMS.Application.Interfaces.Services;
using EPPMS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static EPPMS.Infrastructure.Data.StoredProcedureNames;

namespace EPPMS.Portal.Areas.Admin.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly ILookupService _lookupService;

        public CreateModel(ITaskService taskService, ILookupService lookupService)
        {
            ArgumentNullException.ThrowIfNull(lookupService);
            _taskService = taskService;
            _lookupService = lookupService; 
        }

        [BindProperty]
        public TaskCreateDTO Task { get; set; } = new();
        public IReadOnlyList<LookupResponseDTO> Priorities { get; private set; } = [];
        public IReadOnlyList<LookupResponseDTO> Statuses { get; private set; } = [];
        public IReadOnlyList<ModulesLookupResponseDTO> Feature { get; private set; } = [];
        public IReadOnlyList<ModulesLookupResponseDTO> TechnicalModule { get; private set; } = [];
        public IReadOnlyList<ModulesLookupResponseDTO> Bug { get; private set; } = [];

        public async System.Threading.Tasks.Task OnGetAsync(CancellationToken cancellationToken)
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
                await _taskService.CreateTaskAsync(Task);

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

        private async System.Threading.Tasks.Task LoadLookupsAsync(CancellationToken cancellationToken)
        {   
            Priorities = await _lookupService.GetPrioritiesAsync(cancellationToken);
            Statuses = await _lookupService.GetStatusesAsync(cancellationToken);
            Feature = await _lookupService.GetFeatureAsync(cancellationToken);
            TechnicalModule = await _lookupService.GetTechnicalModuleAsync(cancellationToken);
            Bug =   await _lookupService.GetBugAsync(cancellationToken);
        }
    }
}
