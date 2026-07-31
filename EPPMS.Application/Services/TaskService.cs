using EPPMS.Application.DTOs.Task;
using EPPMS.Application.Exceptions;
using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Application.Interfaces.Services;

namespace EPPMS.Application.Services
{
    public sealed class TaskService : ITaskService
    {
        #region Fields

        private readonly ITaskRepository _taskRepository;

        #endregion

        #region Constructor

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        #endregion

        #region Queries
        public async Task<List<TaskDetailsDTO>> GetTasksAsync(
            Guid? taskId = null,
            Guid? featureId = null,
            Guid? techModuleId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true)
        {
            return await _taskRepository.GetTasksAsync(
                taskId,
                featureId,
                techModuleId,
                priorityId,
                statusId,
                isActive);
        }

        public async Task<TaskDetailsDTO> GetTaskDetailsAsync(Guid taskId)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);

            if (task is null)
            {
                throw new NotFoundException($"Task '{taskId}' was not found.");
            }

            return task;
        }
        #endregion

        #region Commands
        public async Task<bool> CreateTaskAsync(TaskCreateDTO task)
        {
            return await _taskRepository.CreateAsync(task);
        }
        public async Task<bool> UpdateTaskAsync(TaskUpdateDTO task)
        {
            return await _taskRepository.UpdateAsync(task);
        }
        public async Task<bool> DeleteTaskAsync(Guid taskId, string updatedBy)
        {
            return await _taskRepository.DeleteAsync(taskId, updatedBy);
        }
        #endregion
    }
}