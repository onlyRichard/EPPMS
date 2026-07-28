using EPPMS.Portal.ViewModels.Feature;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Features;

public class CreateModel : PageModel
{
    [BindProperty]
    public FeatureCreateViewModel Feature { get; set; } = new();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Save feature...

        return RedirectToPage("./Index");
    }
}