using EPPMS.Application.DTOs.Application.Requests;
using EPPMS.Application.DTOs.Common;
using EPPMS.Application.DTOs.Lookup;
using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Application.Interfaces.Services;
using EPPMS.Portal.Services.Interfaces;
using EPPMS.Portal.ViewModels.Application;
using EPPMS.Portal.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Applications;

public sealed class CreateModel : PageModel
{
    private readonly ILookupRepository _lookupService;
    private readonly IApplicationService _applicationService;
    public CreateModel(ILookupRepository lookupService, IApplicationService applicationService)
    {
        ArgumentNullException.ThrowIfNull(lookupService);
        _lookupService = lookupService;
        _applicationService = applicationService;
    }

    [BindProperty]
    public ApplicationCreateDTO Application { get; set; } = new();
    public string? ErrorMessage { get; private set; }

    public IReadOnlyList<LookupResponseDTO> CurrentHealths { get; private set; } = [];

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
            await _applicationService.CreateApplicationAsync(Application);

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
        CurrentHealths = await _lookupService.GetCurrentHealthsAsync(cancellationToken);
    }
}