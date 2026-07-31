using EPPMS.Application.DTOs.OngoingTask;
using EPPMS.Application.Exceptions;
using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Application.Interfaces.Services;

namespace EPPMS.Application.Services
{
    public sealed class OngoingTaskService : IOngoingTaskService
    {
        #region Fields

        private readonly IOngoingTaskRepository _ongoingTaskRepository;

        #endregion

        #region Constructor

        public OngoingTaskService(IOngoingTaskRepository ongoingTaskRepository)
        {
            _ongoingTaskRepository = ongoingTaskRepository;
        }

        #endregion

        #region Queries

        public async Task<List<OngoingTaskDetailsDTO>> GetOngoingTasksAsync(
            Guid? ongoingTaskId = null,
            Guid? appId = null,
            int? typeId = null,
            int? severityId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true)
        {
            return await _ongoingTaskRepository.GetOngoingTasksAsync(
                ongoingTaskId,
                appId,
                typeId,
                severityId,
                priorityId,
                statusId,
                isActive);
        }

        public async Task<OngoingTaskDetailsDTO> GetOngoingTaskDetailsAsync(Guid ongoingTaskId)
        {
            var ongoingTask = await _ongoingTaskRepository
                .GetOngoingTaskByIdAsync(ongoingTaskId);

            if (ongoingTask is null)
            {
                throw new NotFoundException($"Ongoing Task '{ongoingTaskId}' was not found.");
            }

            return ongoingTask;
        }
        #endregion

        #region Commands
        public async Task<bool> CreateOngoingTaskAsync(OngoingTaskCreateDTO ongoingTask)
        {
            return await _ongoingTaskRepository.CreateAsync(ongoingTask);
        }
        public async Task<bool> UpdateOngoingTaskAsync(OngoingTaskUpdateDTO ongoingTask)
        {
            return await _ongoingTaskRepository.UpdateAsync(ongoingTask);
        }
        public async Task<bool> DeleteOngoingTaskAsync(Guid ongoingTaskId, string updatedBy)
        {
            return await _ongoingTaskRepository.DeleteAsync(ongoingTaskId, updatedBy);
        }
        #endregion
    }
}