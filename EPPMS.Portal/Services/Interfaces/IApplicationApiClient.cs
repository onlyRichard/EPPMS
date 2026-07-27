using EPPMS.Portal.ViewModels.Application;

namespace EPPMS.Portal.Services.Interfaces;

public interface IApplicationApiClient
{
    #region Queries
    Task<List<ApplicationListViewModel>> GetApplicationsAsync(string? search = null,  int? currentHealthId = null, bool isActive = true, CancellationToken cancellationToken = default);
    Task<ApplicationDetailsViewModel?> GetApplicationAsync(Guid appId, CancellationToken cancellationToken = default);
    #endregion

    #region Commands
    Task CreateApplicationAsync(ApplicationCreateViewModel model, CancellationToken cancellationToken = default);
    Task UpdateApplicationAsync(ApplicationEditViewModel model, CancellationToken cancellationToken = default);
    Task DeleteApplicationAsync(Guid appId, string updatedBy,  CancellationToken cancellationToken = default);
    #endregion
}