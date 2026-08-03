using EPPMS.Application.DTOs.Feature;
using EPPMS.Application.DTOs.Task;
using EPPMS.Application.Interfaces.Services;
using EPPMS.Portal.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Areas.Admin.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ITaskService taskService, ILogger<IndexModel> logger)
        {
            ArgumentNullException.ThrowIfNull(taskService);
            ArgumentNullException.ThrowIfNull(logger);
            _taskService = taskService;
            _logger = logger;
        }

        public List<TaskDetailsDTO> Tasks { get; private set; } = [];

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            try
            {
                Tasks = await _taskService.GetTasksAsync();
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "Failed to load features.");

                TempData["Error"] = ex.Message;

                Tasks = [];
            }
        }
    }
}
