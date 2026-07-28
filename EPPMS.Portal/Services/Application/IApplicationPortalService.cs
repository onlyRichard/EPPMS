using EPPMS.Application.DTOs.Application.Requests;
using EPPMS.Application.DTOs.Application.Response;
using EPPMS.Portal.ViewModels.Application;

namespace EPPMS.Portal.Services.Interfaces;

public interface IApplicationService
{
    #region Queries

    Task<List<ApplicationListResponseDTO>> GetApplicationsAsync(
        string? search = null,
        int? currentHealthId = null,
        bool isActive = true,
        CancellationToken cancellationToken = default);

    Task<ApplicationDetailsResponseDTO?> GetApplicationAsync(Guid appId, CancellationToken cancellationToken = default);
    #endregion

    #region Commands
    Task CreateApplicationAsync(ApplicationCreateDTO model, CancellationToken cancellationToken = default);
    //Task UpdateApplicationAsync(ApplicationEditViewModel model, CancellationToken cancellationToken = default);
    //Task DeleteApplicationAsync(Guid appId, string updatedBy, CancellationToken cancellationToken = default);

    #endregion
}