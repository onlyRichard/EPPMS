using EPPMS.Application.DTOs.Application;
using EPPMS.Application.ProductManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Repositories
{
    public interface IApplicationRepository
    {
        Task<List<ApplicationDetailsDTO>> GetApplicationsAsync(string? search = null,int? currentHealthId = null, bool isActive = true);
        Task<ApplicationDetailsDTO?> GetApplicationByIdAsync(Guid appId);
        Task<bool> CreateAsync(ApplicationCreateDTO application);
        Task<bool> UpdateAsync(ApplicationUpdateDTO application);
        Task<bool> DeleteAsync(Guid appId,string updatedBy);
    }
}
