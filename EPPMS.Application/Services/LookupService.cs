using EPPMS.Application.DTOs.Lookup;
using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Application.Interfaces.Services;

namespace EPPMS.Application.Services
{
    public sealed class LookupService : ILookupService
    {
        #region Fields

        private readonly ILookupRepository _lookupRepository;

        #endregion

        #region Constructor

        public LookupService(ILookupRepository lookupRepository)
        {
            ArgumentNullException.ThrowIfNull(lookupRepository);

            _lookupRepository = lookupRepository;
        }

        #endregion

        #region Queries

        public async Task<IReadOnlyList<LookupResponseDTO>> GetActionTypesAsync(
            CancellationToken cancellationToken)
        {
            return await _lookupRepository.GetActionTypesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<LookupResponseDTO>> GetComplexitiesAsync(
            CancellationToken cancellationToken)
        {
            return await _lookupRepository.GetComplexitiesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<LookupResponseDTO>> GetCurrentHealthsAsync(
            CancellationToken cancellationToken)
        {
            return await _lookupRepository.GetCurrentHealthsAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<LookupResponseDTO>> GetPrioritiesAsync(
            CancellationToken cancellationToken)
        {
            return await _lookupRepository.GetPrioritiesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<LookupResponseDTO>> GetReleaseStatusesAsync(
            CancellationToken cancellationToken)
        {
            return await _lookupRepository.GetReleaseStatusesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<LookupResponseDTO>> GetRequestTypesAsync(
            CancellationToken cancellationToken)
        {
            return await _lookupRepository.GetRequestTypesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<LookupResponseDTO>> GetSeveritiesAsync(
            CancellationToken cancellationToken)
        {
            return await _lookupRepository.GetSeveritiesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<LookupResponseDTO>> GetStatusesAsync(
            CancellationToken cancellationToken)
        {
            return await _lookupRepository.GetStatusesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<LookupResponseDTO>> GetTechnologyAreasAsync(
            CancellationToken cancellationToken)
        {
            return await _lookupRepository.GetTechnologyAreasAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<LookupResponseDTO>> GetTestingStatusesAsync(
            CancellationToken cancellationToken)
        {
            return await _lookupRepository.GetTestingStatusesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<LookupResponseDTO>> GetTypesAsync(
            CancellationToken cancellationToken)
        {
            return await _lookupRepository.GetTypesAsync(cancellationToken);
        }

        #endregion
    }
}