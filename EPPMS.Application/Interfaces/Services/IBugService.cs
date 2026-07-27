using EPPMS.Application.DTOs.Bug;

namespace EPPMS.Application.Interfaces.Services
{
    public interface IBugService
    {
        #region Queries

        Task<List<BugDetailsDTO>> GetBugsAsync(
            Guid? bugId = null,
            Guid? appId = null,
            int? severityId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true);

        Task<BugDetailsDTO> GetBugDetailsAsync(Guid bugId);

        #endregion

        #region Commands

        Task<bool> CreateBugAsync(BugCreateDTO bug);

        Task<bool> UpdateBugAsync(BugUpdateDTO bug);

        Task<bool> DeleteBugAsync(Guid bugId, string updatedBy);

        #endregion
    }
}