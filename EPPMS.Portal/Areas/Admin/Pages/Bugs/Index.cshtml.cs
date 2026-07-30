using EPPMS.Application.DTOs.Bug;
using EPPMS.Application.DTOs.Feature;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Bugs
{
    public class IndexModel : PageModel
    {
        public List<BugCreateDTO> Bug { get; private set; } = [];

        public void OnGet()
        {
        }
    }
}
