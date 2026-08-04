using EPPMS.Application.DTOs.Feature;
using EPPMS.Application.DTOs.Lookup;
using EPPMS.Application.Interfaces.Services;
using EPPMS.Application.Services;
using EPPMS.Portal.ViewModels.Feature;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Features;

public class CreateModel : PageModel
{
    private readonly ILookupService _lookupService;
    private readonly IFeatureService _featureService;

    public CreateModel(ILookupService lookupService, IFeatureService featureService)
    {
        ArgumentNullException.ThrowIfNull(lookupService);
        _featureService = featureService;
        _lookupService = lookupService;
    }

    [BindProperty]
    public FeatureCreateDTO Feature { get; set; } = new();
    public IReadOnlyList<ModulesLookupResponseDTO> Applications { get; private set; } = [];
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

        try
        {
            await _featureService.CreateFeatureAsync(Feature);

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
        Applications = await _lookupService.GetApplicationAsync(cancellationToken);
        RequestTypes = await _lookupService.GetRequestTypesAsync(cancellationToken);
        Priorities = await _lookupService.GetPrioritiesAsync(cancellationToken);
        Complexities = await _lookupService.GetComplexitiesAsync(cancellationToken);
        Statuses = await _lookupService.GetStatusesAsync(cancellationToken);

        // Requesters = await _userService.GetLookupAsync(cancellationToken);
    }
}