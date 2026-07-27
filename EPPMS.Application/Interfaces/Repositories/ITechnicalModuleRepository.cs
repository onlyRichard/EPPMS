using EPPMS.Application.DTOs.TechnicalModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Repositories
{
    public interface ITechnicalModuleRepository
    {
        Task<List<TechnicalModuleDetailsDTO>> GetTechnicalModulesAsync(
           Guid? techModuleId = null,
           int? technologyAreaId = null,
           int? priorityId = null,
           int? statusId = null,
           bool isActive = true);
        Task<TechnicalModuleDetailsDTO?> GetTechnicalModuleByIdAsync(Guid techModuleId);

        Task<bool> CreateAsync(TechnicalModuleCreateDTO technicalModule);

        Task<bool> UpdateAsync(TechnicalModuleUpdateDTO technicalModule);

        Task<bool> DeleteAsync(Guid techModuleId, string updatedBy);
    }
}
