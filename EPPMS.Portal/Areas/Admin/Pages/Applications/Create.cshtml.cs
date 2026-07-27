using EPPMS.Portal.Services.ApiClients;
using EPPMS.Portal.Services.Interfaces;
using EPPMS.Portal.ViewModels.Application;
using EPPMS.Portal.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Applications;

public sealed class CreateModel : PageModel
{
    private readonly ILookupApiClient _lookupApiClient;
    private readonly IApplicationApiClient _applicationApiClient;
    public CreateModel(ILookupApiClient lookupApiClient, IApplicationApiClient applicationApiClient)
    {
        ArgumentNullException.ThrowIfNull(lookupApiClient);
        _lookupApiClient = lookupApiClient;
        _applicationApiClient = applicationApiClient;
    }

    [BindProperty]
    public ApplicationCreateViewModel Application { get; set; } = new();

    public List<LookupViewModel> CurrentHealths { get; private set; } = [];

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
        await _applicationApiClient.CreateApplicationAsync(Application,  cancellationToken);
        return RedirectToPage("./Index");
    }

    private async Task LoadLookupsAsync(CancellationToken cancellationToken)
    {
        CurrentHealths = await _lookupApiClient.GetCurrentHealthsAsync(cancellationToken);
    }
}