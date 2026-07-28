using EPPMS.Portal.Constants;
using EPPMS.Portal.Services.ApiClients.Base;
using EPPMS.Portal.Services.Interfaces;
using EPPMS.Portal.ViewModels.Feature;

namespace EPPMS.Portal.Services.ApiClients;

public sealed class FeatureApiClient : BaseApiClient, IFeatureApiClient
{
    #region Fields
    private readonly ILogger<FeatureApiClient> _logger;
    #endregion

    #region Constructor
    public FeatureApiClient(HttpClient httpClient, ILogger<FeatureApiClient> logger) : base(httpClient)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
    }
    #endregion

    #region Queries
    public async Task<List<FeatureListViewModel>> GetFeaturesAsync(
        string? search = null,
        Guid? appId = null,
        int? priorityId = null,
        int? statusId = null,
        int? requestTypeId = null,
        bool isActive = true,
        CancellationToken cancellationToken = default)
    {
        var url =
            $"{ApiRoutes.Features.Base}" +
            $"?search={Uri.EscapeDataString(search ?? string.Empty)}" +
            $"&appId={appId}" +
            $"&priorityId={priorityId}" +
            $"&statusId={statusId}" +
            $"&requestTypeId={requestTypeId}" +
            $"&isActive={isActive}";

        return await GetAsync<List<FeatureListViewModel>>(url, cancellationToken) ?? [];
    }

    public async Task<FeatureDetailsViewModel?> GetFeatureAsync(Guid featureId,  CancellationToken cancellationToken = default)
    {
        return await GetAsync<FeatureDetailsViewModel>($"{ApiRoutes.Features.Base}/{featureId}",  cancellationToken);
    }

    #endregion

    #region Commands
    public async Task CreateFeatureAsync(FeatureCreateViewModel model, CancellationToken cancellationToken = default)
    {
        await PostAsync(ApiRoutes.Features.Base, model, cancellationToken);
    }

    public async Task UpdateFeatureAsync(FeatureEditViewModel model, CancellationToken cancellationToken = default)
    {
        await PutAsync(ApiRoutes.Features.Base,  model,  cancellationToken);
    }

    public async Task DeleteFeatureAsync(Guid featureId,string updatedBy, CancellationToken cancellationToken = default)
    {
        await DeleteAsync($"{ApiRoutes.Features.Base}/{featureId}?updatedBy={Uri.EscapeDataString(updatedBy)}",  cancellationToken);
    }

    #endregion
}