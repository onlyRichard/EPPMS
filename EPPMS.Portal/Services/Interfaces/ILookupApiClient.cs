using EPPMS.Portal.ViewModels.Common;

namespace EPPMS.Portal.Services.Interfaces;

public interface ILookupApiClient
{
    Task<List<LookupViewModel>> GetCurrentHealthsAsync(CancellationToken cancellationToken = default);
    Task<List<LookupViewModel>> GetPrioritiesAsync(CancellationToken cancellationToken = default);
    Task<List<LookupViewModel>> GetStatusesAsync(CancellationToken cancellationToken = default);
    Task<List<LookupViewModel>> GetSeveritiesAsync(CancellationToken cancellationToken = default);
}