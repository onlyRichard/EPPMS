using EPPMS.Application.DTOs.Bug;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Repositories
{
    public interface IBugRepository
    {
        Task<List<BugDetailsDTO>> GetBugsAsync(
            Guid? bugId = null,
            Guid? appId = null,
            int? severityId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true);
        Task<BugDetailsDTO?> GetBugByIdAsync(Guid bugId);
        Task<bool> CreateAsync(BugCreateDTO bug);
        Task<bool> UpdateAsync(BugUpdateDTO bug);
        Task<bool> DeleteAsync(Guid bugId, string updatedBy);
    }
}
