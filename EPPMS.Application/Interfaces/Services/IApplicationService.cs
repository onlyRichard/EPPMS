using EPPMS.Application.DTOs.Application;
using EPPMS.Application.DTOs.Application.Requests;
using EPPMS.Application.DTOs.Application.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Services
{
    public interface IApplicationService
    {
        #region Queries
        Task<List<ApplicationListResponseDTO>> GetApplicationsAsync(string? search = null, int? currentHealthId = null, bool isActive = true);
        Task<ApplicationListResponseDTO> GetApplicationDetailsAsync(Guid appId);
        #endregion

        #region Commands
        Task<bool> CreateApplicationAsync(ApplicationCreateDTO application);
        Task<bool> UpdateApplicationAsync(ApplicationUpdateDTO application);
        Task<bool> DeleteApplicationAsync(Guid appId, string updatedBy);
        #endregion
    }
}
