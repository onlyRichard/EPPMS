using EPPMS.Application.DTOs.Task;

namespace EPPMS.Application.Interfaces.Services
{
    public interface ITaskService
    {
        #region Queries

        Task<List<TaskDetailsDTO>> GetTasksAsync(
            Guid? taskId = null,
            Guid? featureId = null,
            Guid? techModuleId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true);

        Task<TaskDetailsDTO> GetTaskDetailsAsync(Guid taskId);

        #endregion

        #region Commands
        Task<bool> CreateTaskAsync(TaskCreateDTO task);
        Task<bool> UpdateTaskAsync(TaskUpdateDTO task);
        Task<bool> DeleteTaskAsync(Guid taskId, string updatedBy);
        #endregion
    }
}