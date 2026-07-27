using EPPMS.API.Controllers.Base;
using EPPMS.Application.DTOs.Lookup;
using EPPMS.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPPMS.API.Controllers.Admin
{ 
    public sealed class LookupController : AdminBaseApiController
    {
        private readonly ILookupRepository _lookupRepository;

        public LookupController(ILookupRepository lookupRepository)
        {
            ArgumentNullException.ThrowIfNull(lookupRepository);

            _lookupRepository = lookupRepository;
        }

        #region Action Types

        [HttpGet("action-types")]
        [ProducesResponseType(typeof(IReadOnlyList<LookupResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LookupResponseDTO>>> GetActionTypesAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _lookupRepository.GetActionTypesAsync(cancellationToken));
        }

        #endregion

        #region Complexities

        [HttpGet("complexities")]
        [ProducesResponseType(typeof(IReadOnlyList<LookupResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LookupResponseDTO>>> GetComplexitiesAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _lookupRepository.GetComplexitiesAsync(cancellationToken));
        }

        #endregion

        #region Current Health

        [HttpGet("current-healths")]
        [ProducesResponseType(typeof(IReadOnlyList<LookupResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LookupResponseDTO>>> GetCurrentHealthsAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _lookupRepository.GetCurrentHealthsAsync(cancellationToken));
        }

        #endregion

        #region Priorities

        [HttpGet("priorities")]
        [ProducesResponseType(typeof(IReadOnlyList<LookupResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LookupResponseDTO>>> GetPrioritiesAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _lookupRepository.GetPrioritiesAsync(cancellationToken));
        }

        #endregion

        #region Release Statuses

        [HttpGet("release-statuses")]
        [ProducesResponseType(typeof(IReadOnlyList<LookupResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LookupResponseDTO>>> GetReleaseStatusesAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _lookupRepository.GetReleaseStatusesAsync(cancellationToken));
        }

        #endregion

        #region Request Types

        [HttpGet("request-types")]
        [ProducesResponseType(typeof(IReadOnlyList<LookupResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LookupResponseDTO>>> GetRequestTypesAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _lookupRepository.GetRequestTypesAsync(cancellationToken));
        }

        #endregion

        #region Severities

        [HttpGet("severities")]
        [ProducesResponseType(typeof(IReadOnlyList<LookupResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LookupResponseDTO>>> GetSeveritiesAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _lookupRepository.GetSeveritiesAsync(cancellationToken));
        }

        #endregion

        #region Statuses

        [HttpGet("statuses")]
        [ProducesResponseType(typeof(IReadOnlyList<LookupResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LookupResponseDTO>>> GetStatusesAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _lookupRepository.GetStatusesAsync(cancellationToken));
        }

        #endregion

        #region Technology Areas

        [HttpGet("technology-areas")]
        [ProducesResponseType(typeof(IReadOnlyList<LookupResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LookupResponseDTO>>> GetTechnologyAreasAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _lookupRepository.GetTechnologyAreasAsync(cancellationToken));
        }

        #endregion

        #region Testing Statuses

        [HttpGet("testing-statuses")]
        [ProducesResponseType(typeof(IReadOnlyList<LookupResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LookupResponseDTO>>> GetTestingStatusesAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _lookupRepository.GetTestingStatusesAsync(cancellationToken));
        }

        #endregion

        #region Types

        [HttpGet("types")]
        [ProducesResponseType(typeof(IReadOnlyList<LookupResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LookupResponseDTO>>> GetTypesAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _lookupRepository.GetTypesAsync(cancellationToken));
        }

        #endregion
    }
}