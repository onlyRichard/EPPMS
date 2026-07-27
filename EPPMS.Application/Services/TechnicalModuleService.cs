using EPPMS.Application.DTOs.TechnicalModule;
using EPPMS.Application.Exceptions;
using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Application.Interfaces.Services;

namespace EPPMS.Application.Services
{
    public sealed class TechnicalModuleService : ITechnicalModuleService
    {
        #region Fields

        private readonly ITechnicalModuleRepository _technicalModuleRepository;

        #endregion

        #region Constructor

        public TechnicalModuleService(
            ITechnicalModuleRepository technicalModuleRepository)
        {
            _technicalModuleRepository = technicalModuleRepository;
        }

        #endregion

        #region Queries

        public async Task<List<TechnicalModuleDetailsDTO>> GetTechnicalModulesAsync(
            Guid? techModuleId = null,
            int? technologyAreaId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true)
        {
            return await _technicalModuleRepository.GetTechnicalModulesAsync(
                techModuleId,
                technologyAreaId,
                priorityId,
                statusId,
                isActive);
        }

        public async Task<TechnicalModuleDetailsDTO> GetTechnicalModuleDetailsAsync(Guid techModuleId)
        {
            var technicalModule = await _technicalModuleRepository.GetTechnicalModuleByIdAsync(techModuleId);

            if (technicalModule is null)
            {
                throw new NotFoundException($"Technical Module '{techModuleId}' was not found.");
            }
            return technicalModule;
        }

        #endregion

        #region Commands

        public async Task<bool> CreateTechnicalModuleAsync(TechnicalModuleCreateDTO technicalModule)
        {
            return await _technicalModuleRepository.CreateAsync(technicalModule);
        }

        public async Task<bool> UpdateTechnicalModuleAsync(TechnicalModuleUpdateDTO technicalModule)
        {
            return await _technicalModuleRepository.UpdateAsync(technicalModule);
        }

        public async Task<bool> DeleteTechnicalModuleAsync(Guid techModuleId, string updatedBy)
        {
            return await _technicalModuleRepository.DeleteAsync(techModuleId, updatedBy);
        }

        #endregion
    }
}