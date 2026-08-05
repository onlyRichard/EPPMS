using EPPMS.Application.DTOs.Error;
using EPPMS.Application.Exceptions;
using EPPMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPPMS.Portal.Pages.Error;

public sealed class IndexModel : PageModel
{
    private readonly IErrorPageService _errorPageService;

    public IndexModel(
        IErrorPageService errorPageService)
    {
        ArgumentNullException.ThrowIfNull(errorPageService);

        _errorPageService = errorPageService;
    }

    public ErrorPageDTO Error { get; private set; } = new();

    public async Task OnGetAsync(
        int? statusCode,
        CancellationToken cancellationToken)
    {
        int resolvedStatusCode = statusCode ?? 500;

        IExceptionHandlerPathFeature? exceptionFeature =
            HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionFeature is not null)
        {
            if (exceptionFeature.Error is BusinessRuleException businessException)
            {
                resolvedStatusCode = businessException.StatusCode;
            }
            else
            {
                resolvedStatusCode = 500;
            }
        }

        Error = await _errorPageService.GetErrorPageAsync(
            resolvedStatusCode,
            cancellationToken);

        Error.ErrorReference = HttpContext.TraceIdentifier;
    }
}