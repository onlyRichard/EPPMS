using EPPMS.Application.DTOs.Bug;
using EPPMS.Application.Exceptions;
using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Application.Interfaces.Services;

namespace EPPMS.Application.Services
{
    public sealed class BugService : IBugService
    {
        #region Fields

        private readonly IBugRepository _bugRepository;

        #endregion

        #region Constructor

        public BugService(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        #endregion

        #region Queries
        public async Task<List<BugDetailsDTO>> GetBugsAsync(
            Guid? bugId = null,
            Guid? appId = null,
            int? severityId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true)
        {
            return await _bugRepository.GetBugsAsync(
                bugId,
                appId,
                severityId,
                priorityId,
                statusId,
                isActive);
        }

        public async Task<BugDetailsDTO> GetBugDetailsAsync(Guid bugId)
        {
            var bug = await _bugRepository.GetBugByIdAsync(bugId);

            if (bug is null)
            {
                throw new NotFoundException($"Bug '{bugId}' was not found.");
            }
            return bug;
        }
        #endregion

        #region Commands
        public async Task<bool> CreateBugAsync(BugCreateDTO bug)
        {
            return await _bugRepository.CreateAsync(bug);
        }
        public async Task<bool> UpdateBugAsync(BugUpdateDTO bug)
        {
            return await _bugRepository.UpdateAsync(bug);
        }
        public async Task<bool> DeleteBugAsync(Guid bugId, string updatedBy)
        {
            return await _bugRepository.DeleteAsync(bugId, updatedBy);
        }
        #endregion
    }
}