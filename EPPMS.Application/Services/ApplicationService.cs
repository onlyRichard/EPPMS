using EPPMS.Application.DTOs.Application;
using EPPMS.Application.DTOs.Application.Requests;
using EPPMS.Application.DTOs.Application.Response;
using EPPMS.Application.Exceptions;
using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Application.Interfaces.Services;

namespace EPPMS.Application.Services
{
    public sealed class ApplicationService : IApplicationService
    {
        #region Fields

        private readonly IApplicationRepository _applicationRepository;

        #endregion

        #region Constructor

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        #endregion

        #region Queries

        public async Task<List<ApplicationListResponseDTO>> GetApplicationsAsync(
            string? search = null,
            int? currentHealthId = null,
            bool isActive = true)
        {
            return await _applicationRepository.GetApplicationsAsync(
                search,
                currentHealthId,
                isActive);
        }

        public async Task<ApplicationListResponseDTO> GetApplicationDetailsAsync(Guid appId)
        {
            var application = await _applicationRepository.GetApplicationByIdAsync(appId);

            if (application is null)
            {
                throw new NotFoundException(
                    $"Application '{appId}' was not found.");
            }

            return application;
        }

        #endregion

        #region Commands

        public async Task<bool> CreateApplicationAsync(ApplicationCreateDTO application)
        {
            return await _applicationRepository.CreateAsync(application);
        }

        public async Task<bool> UpdateApplicationAsync(ApplicationUpdateDTO application)
        {
            return await _applicationRepository.UpdateAsync(application);
        }

        public async Task<bool> DeleteApplicationAsync(Guid appId, string updatedBy)
        {
            return await _applicationRepository.DeleteAsync(appId, updatedBy);
        }

        #endregion
    }
}