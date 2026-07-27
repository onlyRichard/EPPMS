using EPPMS.Application.DTOs.Feature;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Repositories
{
    public interface IFeatureRepository
    {
        Task<List<FeatureDetailsDTO>> GetFeaturesAsync(
        string? search = null,
        Guid? appId = null,
        int? priorityId = null,
        int? statusId = null,
        int? requestTypeId = null,
        bool isActive = true);

        Task<FeatureDetailsDTO?> GetFeatureByIdAsync(Guid featureId);
        Task<bool> CreateAsync(FeatureCreateDTO feature);
        Task<bool> UpdateAsync(FeatureUpdateDTO feature);
        Task<bool> DeleteAsync(Guid featureId,string updatedBy);
    }
}
