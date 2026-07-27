using EPPMS.Application.DTOs.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskDetailsDTO>> GetTasksAsync(
            Guid? taskId = null,
            Guid? featureId = null,
            Guid? techModuleId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true);
        Task<TaskDetailsDTO?> GetTaskByIdAsync(Guid taskId);
        Task<bool> CreateAsync(TaskCreateDTO task);
        Task<bool> UpdateAsync(TaskUpdateDTO task);
        Task<bool> DeleteAsync(Guid taskId, string updatedBy);
    }
}
