using EPPMS.API.Controllers.Base;
using EPPMS.Application.DTOs.Feature;
using EPPMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EPPMS.API.Controllers.Admin.ProductManagement;

public sealed class FeaturesController : AdminBaseApiController
{
    #region Fields

    private readonly IFeatureService _featureService;

    #endregion

    #region Constructor

    public FeaturesController(IFeatureService featureService)
    {
        ArgumentNullException.ThrowIfNull(featureService);

        _featureService = featureService;
    }

    #endregion

    #region Queries

    [HttpGet]
    [ProducesResponseType(typeof(List<FeatureDetailsDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<FeatureDetailsDTO>>> GetFeatures(
        [FromQuery] string? search = null,
        [FromQuery] Guid? appId = null,
        [FromQuery] int? priorityId = null,
        [FromQuery] int? statusId = null,
        [FromQuery] int? requestTypeId = null,
        [FromQuery] bool isActive = true)
    {
        List<FeatureDetailsDTO> features =
            await _featureService.GetFeaturesAsync(
                search,
                appId,
                priorityId,
                statusId,
                requestTypeId,
                isActive);

        return Ok(features);
    }

    [HttpGet("{featureId:guid}")]
    [ProducesResponseType(typeof(FeatureDetailsDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FeatureDetailsDTO>> GetFeatureById(Guid featureId)
    {
        FeatureDetailsDTO feature = await _featureService.GetFeatureDetailsAsync(featureId);
        return Ok(feature);
    }

    #endregion

    #region Commands

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateFeature([FromBody] FeatureCreateDTO feature)
    {
        bool created = await _featureService.CreateFeatureAsync(feature);
        return CreatedAtAction(nameof(GetFeatureById), new { featureId = feature.FeatureId },  created);
    }

    [HttpPut("{featureId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateFeature(Guid featureId,[FromBody] FeatureUpdateDTO feature)
    {
        feature.FeatureId = featureId;
        bool updated = await _featureService.UpdateFeatureAsync(feature);
        return Ok(updated);
    }

    [HttpDelete("{featureId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteFeature(Guid featureId, [FromQuery] string updatedBy)
    {
        await _featureService.DeleteFeatureAsync(featureId, updatedBy);
        return NoContent();
    }

    #endregion
}