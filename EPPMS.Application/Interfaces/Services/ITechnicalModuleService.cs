using EPPMS.Application.DTOs.TechnicalModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Services
{
    public interface ITechnicalModuleService
    {
        Task<List<TechnicalModuleDetailsDTO>> GetTechnicalModulesAsync(
            Guid? techModuleId = null,
            int? technologyAreaId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true);
        Task<TechnicalModuleDetailsDTO?> GetTechnicalModuleDetailsAsync(Guid techModuleId);
        Task<bool> CreateTechnicalModuleAsync(TechnicalModuleCreateDTO technicalModule);
        Task<bool> UpdateTechnicalModuleAsync(TechnicalModuleUpdateDTO technicalModule);
        Task<bool> DeleteTechnicalModuleAsync(Guid techModuleId, string updatedBy);
    }
}
