using EPPMS.Portal.ViewModels.Feature;

namespace EPPMS.Portal.Services.Interfaces;

public interface IFeatureApiClient
{
    #region Queries
    Task<List<FeatureListViewModel>> GetFeaturesAsync(
        string? search = null,
        Guid? appId = null,
        int? priorityId = null,
        int? statusId = null,
        int? requestTypeId = null,
        bool isActive = true,
        CancellationToken cancellationToken = default);

    Task<FeatureDetailsViewModel?> GetFeatureAsync(Guid featureId,CancellationToken cancellationToken = default);
    #endregion

    #region Commands
    Task CreateFeatureAsync(FeatureCreateViewModel model, CancellationToken cancellationToken = default);
    Task UpdateFeatureAsync(FeatureEditViewModel model, CancellationToken cancellationToken = default);
    Task DeleteFeatureAsync(Guid featureId, string updatedBy,  CancellationToken cancellationToken = default);
    #endregion
}