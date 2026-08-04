using EPPMS.Application.DTOs.Bug;
using EPPMS.Application.DTOs.Lookup;
using EPPMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Bug;

public class CreateModel : PageModel
{
    private readonly ILookupService _lookupService;
    private readonly IBugService _bugService;

    public CreateModel(
        ILookupService lookupService,
        IBugService bugService)
    {
        ArgumentNullException.ThrowIfNull(lookupService);
        ArgumentNullException.ThrowIfNull(bugService);

        _lookupService = lookupService;
        _bugService = bugService;
    }

    [BindProperty]
    public BugCreateDTO Bug { get; set; } = new();

    public IReadOnlyList<ModulesLookupResponseDTO> Applications { get; private set; } = [];
    public IReadOnlyList<LookupResponseDTO> Severities { get; private set; } = [];
    public IReadOnlyList<LookupResponseDTO> Priorities { get; private set; } = [];
    public IReadOnlyList<LookupResponseDTO> Statuses { get; private set; } = [];
    public IReadOnlyList<LookupResponseDTO> ReleaseStatuses { get; private set; } = [];
    public IReadOnlyList<LookupResponseDTO> TestingStatuses { get; private set; } = [];

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
            await _bugService.CreateBugAsync(Bug);

            TempData["Success"] = "Bug created successfully.";

            return RedirectToPage("./Index");
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.InnerException?.Message ?? ex.Message;

            await LoadLookupsAsync(cancellationToken);

            return Page();
        }
    }

    private async Task LoadLookupsAsync(CancellationToken cancellationToken)
    {
        Applications = await _lookupService.GetApplicationAsync(cancellationToken);

        Severities = await _lookupService.GetSeveritiesAsync(cancellationToken);

        Priorities = await _lookupService.GetPrioritiesAsync(cancellationToken);

        Statuses = await _lookupService.GetStatusesAsync(cancellationToken);

        ReleaseStatuses = await _lookupService.GetReleaseStatusesAsync(cancellationToken);

        TestingStatuses = await _lookupService.GetTestingStatusesAsync(cancellationToken);
    }
}