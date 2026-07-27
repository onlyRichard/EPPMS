using EPPMS.Application.DTOs.OngoingTask;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Repositories
{
    public interface IOngoingTaskRepository
    {
        Task<List<OngoingTaskDetailsDTO>> GetOngoingTasksAsync(
            Guid? ongoingTaskId = null,
            Guid? appId = null,
            int? typeId = null,
            int? severityId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true);
        Task<OngoingTaskDetailsDTO?> GetOngoingTaskByIdAsync(Guid ongoingTaskId);
        Task<bool> CreateAsync(OngoingTaskCreateDTO ongoingTask);
        Task<bool> UpdateAsync(OngoingTaskUpdateDTO ongoingTask);
        Task<bool> DeleteAsync(Guid ongoingTaskId, string updatedBy);
    }
}
