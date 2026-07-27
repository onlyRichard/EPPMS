using EPPMS.API.Controllers.Base;
using EPPMS.Application.DTOs.Application;
using EPPMS.Application.DTOs.Application.Requests;
using EPPMS.Application.DTOs.Application.Response;
using EPPMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EPPMS.API.Controllers.Admin
{   
    public sealed class ApplicationsController : AdminBaseApiController
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            ArgumentNullException.ThrowIfNull(applicationService);

            _applicationService = applicationService;
        }

        #region Queries

        [HttpGet]
        [ProducesResponseType(typeof(List<ApplicationListResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ApplicationListResponseDTO>>> GetApplications(
            [FromQuery] string? search = null,
            [FromQuery] int? currentHealthId = null,
            [FromQuery] bool isActive = true)
        {
            List<ApplicationListResponseDTO> applications =
                await _applicationService.GetApplicationsAsync(
                    search,
                    currentHealthId,
                    isActive);

            return Ok(applications);
        }

        [HttpGet("{appId:guid}")]
        [ProducesResponseType(typeof(ApplicationListResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplicationListResponseDTO>> GetApplicationById(Guid appId)
        {
            ApplicationListResponseDTO application =
                await _applicationService.GetApplicationDetailsAsync(appId);

            return Ok(application);
        }

        #endregion

        #region Commands

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateApplication(
            [FromBody] ApplicationCreateDTO application)
        {
            bool created =
                await _applicationService.CreateApplicationAsync(application);

            return CreatedAtAction(
                nameof(GetApplicationById),
                new { appId = application.AppId },
                created);
        }

        [HttpPut("{appId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateApplication(
            Guid appId,
            [FromBody] ApplicationUpdateDTO application)
        {
            application.AppId = appId;

            bool updated =
                await _applicationService.UpdateApplicationAsync(application);

            return Ok(updated);
        }

        [HttpDelete("{appId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteApplication(
            Guid appId,
            [FromQuery] string updatedBy)
        {
            await _applicationService.DeleteApplicationAsync(
                appId,
                updatedBy);

            return NoContent();
        }

        #endregion
    }
}