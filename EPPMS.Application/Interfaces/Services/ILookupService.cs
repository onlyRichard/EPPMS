using EPPMS.Application.DTOs.Lookup;

namespace EPPMS.Application.Interfaces.Services
{
    public interface ILookupService
    {
        Task<IReadOnlyList<LookupResponseDTO>> GetActionTypesAsync(
            CancellationToken cancellationToken);

        Task<IReadOnlyList<LookupResponseDTO>> GetComplexitiesAsync(
            CancellationToken cancellationToken);

        Task<IReadOnlyList<LookupResponseDTO>> GetCurrentHealthsAsync(
            CancellationToken cancellationToken);

        Task<IReadOnlyList<LookupResponseDTO>> GetPrioritiesAsync(
            CancellationToken cancellationToken);

        Task<IReadOnlyList<LookupResponseDTO>> GetReleaseStatusesAsync(
            CancellationToken cancellationToken);

        Task<IReadOnlyList<LookupResponseDTO>> GetRequestTypesAsync(
            CancellationToken cancellationToken);

        Task<IReadOnlyList<LookupResponseDTO>> GetSeveritiesAsync(
            CancellationToken cancellationToken);

        Task<IReadOnlyList<LookupResponseDTO>> GetStatusesAsync(
            CancellationToken cancellationToken);

        Task<IReadOnlyList<LookupResponseDTO>> GetTechnologyAreasAsync(
            CancellationToken cancellationToken);

        Task<IReadOnlyList<LookupResponseDTO>> GetTestingStatusesAsync(
            CancellationToken cancellationToken);

        Task<IReadOnlyList<LookupResponseDTO>> GetTypesAsync(
            CancellationToken cancellationToken);
    }
}