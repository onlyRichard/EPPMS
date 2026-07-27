using EPPMS.Application.DTOs.Feature;

namespace EPPMS.Application.Interfaces.Services
{
    public interface IFeatureService
    {
        #region Queries
        Task<List<FeatureDetailsDTO>> GetFeaturesAsync(string? search = null,  Guid? appId = null, int? priorityId = null, int? statusId = null, int? requestTypeId = null,  bool isActive = true);
        Task<FeatureDetailsDTO> GetFeatureDetailsAsync(Guid featureId);
        #endregion

        #region Commands
        Task<bool> CreateFeatureAsync(FeatureCreateDTO feature);
        Task<bool> UpdateFeatureAsync(FeatureUpdateDTO feature);
        Task<bool> DeleteFeatureAsync(Guid featureId, string updatedBy);
        #endregion
    }
}