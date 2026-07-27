using EPPMS.Application.DTOs.Feature;
using EPPMS.Application.Exceptions;
using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Application.Interfaces.Services;

namespace EPPMS.Application.Services
{
    public sealed class FeatureService : IFeatureService
    {
        #region Fields
        private readonly IFeatureRepository _featureRepository;
        #endregion

        #region Constructor
        public FeatureService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }

        #endregion

        #region Queries
        public async Task<List<FeatureDetailsDTO>> GetFeaturesAsync(
            string? search = null,
            Guid? appId = null,
            int? priorityId = null,
            int? statusId = null,
            int? requestTypeId = null,
            bool isActive = true)
        {
            return await _featureRepository.GetFeaturesAsync(
                search,
                appId,
                priorityId,
                statusId,
                requestTypeId,
                isActive);
        }

        public async Task<FeatureDetailsDTO> GetFeatureDetailsAsync(Guid featureId)
        {
            var feature = await _featureRepository.GetFeatureByIdAsync(featureId);

            if (feature is null)
            {
                throw new NotFoundException($"Feature '{featureId}' was not found.");
            }
            return feature;
        }

        #endregion

        #region Commands
        public async Task<bool> CreateFeatureAsync(FeatureCreateDTO feature)
        {
            return await _featureRepository.CreateAsync(feature);
        }
        public async Task<bool> UpdateFeatureAsync(FeatureUpdateDTO feature)
        {
            return await _featureRepository.UpdateAsync(feature);
        }
        public async Task<bool> DeleteFeatureAsync(Guid featureId, string updatedBy)
        {
            return await _featureRepository.DeleteAsync(featureId, updatedBy);
        }
        #endregion
    }
}