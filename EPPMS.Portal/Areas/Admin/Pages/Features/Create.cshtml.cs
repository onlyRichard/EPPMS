using EPPMS.Application.DTOs.Lookup;
using EPPMS.Application.Interfaces.Services;
using EPPMS.Portal.ViewModels.Feature;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Features;

public class CreateModel : PageModel
{
    private readonly ILookupService _lookupService;

    public CreateModel(ILookupService lookupService)
    {
        ArgumentNullException.ThrowIfNull(lookupService);

        _lookupService = lookupService;
    }

    [BindProperty]
    public FeatureCreateViewModel Feature { get; set; } = new();

    public IReadOnlyList<LookupResponseDTO> Applications { get; private set; } = [];

    public IReadOnlyList<LookupResponseDTO> RequestTypes { get; private set; } = [];

    public IReadOnlyList<LookupResponseDTO> Priorities { get; private set; } = [];

    public IReadOnlyList<LookupResponseDTO> Complexities { get; private set; } = [];

    public IReadOnlyList<LookupResponseDTO> Statuses { get; private set; } = [];

    public IReadOnlyList<LookupResponseDTO> Requesters { get; private set; } = [];

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

        // Save Feature

        return RedirectToPage("./Index");
    }

    private async Task LoadLookupsAsync(CancellationToken cancellationToken)
    {
        //Applications = await _lookupService.GetApplicationsAsync(cancellationToken);

        RequestTypes = await _lookupService.GetRequestTypesAsync(cancellationToken);

        Priorities = await _lookupService.GetPrioritiesAsync(cancellationToken);

        Complexities = await _lookupService.GetComplexitiesAsync(cancellationToken);

        Statuses = await _lookupService.GetStatusesAsync(cancellationToken);

        //Requesters = await _lookupService.GetRequestersAsync(cancellationToken);
    }
}