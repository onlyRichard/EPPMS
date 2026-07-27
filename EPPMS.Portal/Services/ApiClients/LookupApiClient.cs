using EPPMS.Portal.Constants;
using EPPMS.Portal.Services.Interfaces;
using EPPMS.Portal.ViewModels.Common;

namespace EPPMS.Portal.Services.ApiClients;

public sealed class LookupApiClient
    : BaseApiClient, ILookupApiClient
{
    public LookupApiClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<List<LookupViewModel>> GetCurrentHealthsAsync(
        CancellationToken cancellationToken = default)
        => GetAsync<List<LookupViewModel>>(
            Lookups.CurrentHealths,
            cancellationToken);

    public Task<List<LookupViewModel>> GetPrioritiesAsync(
        CancellationToken cancellationToken = default)
        => GetAsync<List<LookupViewModel>>(
            Lookups.Priorities,
            cancellationToken);

    public Task<List<LookupViewModel>> GetStatusesAsync(
        CancellationToken cancellationToken = default)
        => GetAsync<List<LookupViewModel>>(
            Lookups.Statuses,
            cancellationToken);

    public Task<List<LookupViewModel>> GetSeveritiesAsync(
        CancellationToken cancellationToken = default)
        => GetAsync<List<LookupViewModel>>(
            Lookups.Severities,
            cancellationToken);
}