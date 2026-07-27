using EPPMS.Application.DTOs.OngoingTask;

namespace EPPMS.Application.Interfaces.Services
{
    public interface IOngoingTaskService
    {
        #region Queries

        Task<List<OngoingTaskDetailsDTO>> GetOngoingTasksAsync(
            Guid? ongoingTaskId = null,
            Guid? appId = null,
            int? typeId = null,
            int? severityId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true);

        Task<OngoingTaskDetailsDTO> GetOngoingTaskDetailsAsync(Guid ongoingTaskId);
        #endregion

        #region Commands
        Task<bool> CreateOngoingTaskAsync(OngoingTaskCreateDTO ongoingTask);
        Task<bool> UpdateOngoingTaskAsync(OngoingTaskUpdateDTO ongoingTask);
        Task<bool> DeleteOngoingTaskAsync(Guid ongoingTaskId, string updatedBy);
        #endregion
    }
}