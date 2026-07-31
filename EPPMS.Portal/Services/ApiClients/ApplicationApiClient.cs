using EPPMS.Portal.Constants;
using EPPMS.Portal.Helpers;
using EPPMS.Portal.Services.ApiClients.Base;
using EPPMS.Portal.Services.Application;
using EPPMS.Portal.ViewModels.Application;

namespace EPPMS.Portal.Services.ApiClients;

public sealed class ApplicationApiClient : BaseApiClient, IApplicationPortalServices
{
    private readonly ILogger<ApplicationApiClient> _logger;

    public ApplicationApiClient(HttpClient httpClient, ILogger<ApplicationApiClient> logger)  : base(httpClient)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
    }

    #region Queries

    public async Task<List<ApplicationListViewModel>> GetApplicationsAsync(
        string? search = null,
        int? currentHealthId = null,
        bool isActive = true,
        CancellationToken cancellationToken = default)
    {
        string requestUri = QueryStringBuilder
            .Create(ApiRoutes.Applications.Base)
            .Add("search", search)
            .Add("currentHealthId", currentHealthId)
            .Add("isActive", isActive)
            .Build();

        return await GetAsync<List<ApplicationListViewModel>>(
                   requestUri,
                   cancellationToken)
               ?? [];
    }

    public async Task<ApplicationDetailsViewModel?> GetApplicationAsync(
        Guid appId,
        CancellationToken cancellationToken = default)
    {
        return await GetAsync<ApplicationDetailsViewModel>(
            $"{ApiRoutes.Applications.Base}/{appId}",
            cancellationToken);
    }

    #endregion

    #region Commands

    public async Task CreateApplicationAsync(
        ApplicationCreateViewModel model,
        CancellationToken cancellationToken = default)
    {
        await PostAsync(
            ApiRoutes.Applications.Base,
            model,
            cancellationToken);
    }

    public async Task UpdateApplicationAsync(
        ApplicationEditViewModel model,
        CancellationToken cancellationToken = default)
    {
        await PutAsync(
            $"{ApiRoutes.Applications.Base}/{model.AppId}",
            model,
            cancellationToken);
    }

    public async Task DeleteApplicationAsync(
        Guid appId,
        string updatedBy,
        CancellationToken cancellationToken = default)
    {
        string requestUri = QueryStringBuilder
            .Create($"{ApiRoutes.Applications.Base}/{appId}")
            .Add("updatedBy", updatedBy)
            .Build();

        await DeleteAsync(
            requestUri,
            cancellationToken);
    }

    #endregion
}