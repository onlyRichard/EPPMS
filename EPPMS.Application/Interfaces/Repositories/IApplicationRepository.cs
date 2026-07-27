using EPPMS.Application.DTOs.Application.Requests;
using EPPMS.Application.DTOs.Application.Response;


namespace EPPMS.Application.Interfaces.Repositories
{
    public interface IApplicationRepository
    {
        Task<List<ApplicationListResponseDTO>> GetApplicationsAsync(string? search = null,int? currentHealthId = null, bool isActive = true);
        Task<ApplicationListResponseDTO?> GetApplicationByIdAsync(Guid appId);
        Task<bool> CreateAsync(ApplicationCreateDTO application);
        Task<bool> UpdateAsync(ApplicationUpdateDTO application);
        Task<bool> DeleteAsync(Guid appId,string updatedBy);
    }
}
