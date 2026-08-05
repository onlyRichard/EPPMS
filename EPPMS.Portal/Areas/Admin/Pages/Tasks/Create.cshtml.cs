using EPPMS.Application.DTOs.Lookup;
using EPPMS.Application.DTOs.Task;
using EPPMS.Application.Exceptions;
using EPPMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Tasks
{
    public sealed class CreateModel : PageModel
    {
        #region Fields

        private readonly ITaskService _taskService;
        private readonly ILookupService _lookupService;

        #endregion

        #region Constructor

        public CreateModel(
            ITaskService taskService,
            ILookupService lookupService)
        {
            ArgumentNullException.ThrowIfNull(taskService);
            ArgumentNullException.ThrowIfNull(lookupService);

            _taskService = taskService;
            _lookupService = lookupService;
        }

        #endregion

        #region Properties

        [BindProperty]
        public TaskCreateDTO Task { get; set; } = new();

        public IReadOnlyList<LookupResponseDTO> Priorities { get; private set; } = [];

        public IReadOnlyList<LookupResponseDTO> Statuses { get; private set; } = [];

        public IReadOnlyList<ModulesLookupResponseDTO> Feature { get; private set; } = [];

        public IReadOnlyList<ModulesLookupResponseDTO> TechnicalModule { get; private set; } = [];

        public IReadOnlyList<ModulesLookupResponseDTO> Bug { get; private set; } = [];

        #endregion

        #region Page Handlers

        public async Task OnGetAsync(
            CancellationToken cancellationToken)
        {
            await LoadLookupsAsync(cancellationToken);
        }

        public async Task<IActionResult> OnPostAsync(
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                await LoadLookupsAsync(cancellationToken);

                return Page();
            }

            try
            {
                await _taskService.CreateTaskAsync(Task);

                TempData["Success"] = "Task created successfully.";

                return RedirectToPage("./Index");
            }
            catch (BusinessRuleException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                await LoadLookupsAsync(cancellationToken);

                return Page();
            }
        }

        #endregion

        #region Private Methods

        private async Task LoadLookupsAsync(
            CancellationToken cancellationToken)
        {
            Priorities = await _lookupService.GetPrioritiesAsync(cancellationToken);
            Statuses = await _lookupService.GetStatusesAsync(cancellationToken);
            Feature = await _lookupService.GetFeatureAsync(cancellationToken);
            TechnicalModule = await _lookupService.GetTechnicalModuleAsync(cancellationToken);
            Bug = await _lookupService.GetBugAsync(cancellationToken);
        }

        #endregion
    }
}