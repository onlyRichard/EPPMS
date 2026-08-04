using EPPMS.Application.DTOs.Lookup;

namespace EPPMS.Application.Interfaces.Repositories
{
    public interface ILookupRepository
    {
        Task<IReadOnlyList<LookupResponseDTO>> GetActionTypesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<LookupResponseDTO>> GetComplexitiesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<LookupResponseDTO>> GetCurrentHealthsAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<LookupResponseDTO>> GetPrioritiesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<LookupResponseDTO>> GetReleaseStatusesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<LookupResponseDTO>> GetRequestTypesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<LookupResponseDTO>> GetSeveritiesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<LookupResponseDTO>> GetStatusesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<LookupResponseDTO>> GetTechnologyAreasAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<LookupResponseDTO>> GetTestingStatusesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<LookupResponseDTO>> GetTypesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<ModulesLookupResponseDTO>> GetApplicationAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<ModulesLookupResponseDTO>> GetFeatureAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<ModulesLookupResponseDTO>> GetTechnicalModuleAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<ModulesLookupResponseDTO>> GetBugAsync(CancellationToken cancellationToken);
    }
}