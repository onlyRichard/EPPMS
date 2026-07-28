

namespace EPPMS.Portal.Services.Application;

public sealed class ApplicationPortalService : IApplicationPortalService
{
    #region Fields

    private readonly EPPMS.Application.Interfaces.Services.IApplicationService _applicationService;
    private readonly ILogger<ApplicationPortalService> _logger;

    #endregion

    #region Constructor

    public ApplicationPortalService(
        EPPMS.Application.Interfaces.Services.IApplicationService applicationService,
        ILogger<ApplicationPortalService> logger)
    {
        ArgumentNullException.ThrowIfNull(applicationService);
        ArgumentNullException.ThrowIfNull(logger);

        _applicationService = applicationService;
        _logger = logger;
    }

    #endregion
}