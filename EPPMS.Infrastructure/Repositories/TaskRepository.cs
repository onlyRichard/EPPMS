using EPPMS.Application.DTOs.Task;
using EPPMS.Application.Interfaces.Data;
using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EPPMS.Infrastructure.Repositories
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public TaskRepository(ISqlConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        #region Queries

        public async Task<List<TaskDetailsDTO>> GetTasksAsync(
            Guid? taskId = null,
            Guid? featureId = null,
            Guid? techModuleId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Task.Get, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(DbParameterExtensions.Create("@TaskId", taskId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@FeatureId", featureId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@TechModuleId", techModuleId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", priorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", statusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@IsActive", isActive, SqlDbType.Bit));

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            List<TaskDetailsDTO> tasks = [];

            while (await reader.ReadAsync())
            {
                tasks.Add(MapTask(reader));
            }

            return tasks;
        }

        public async Task<TaskDetailsDTO?> GetTaskByIdAsync(Guid taskId)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Task.GetById, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(DbParameterExtensions.Create("@TaskId", taskId, SqlDbType.UniqueIdentifier));

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapTask(reader);
            }

            return null;
        }

        #endregion

        #region Commands

        public async Task<bool> CreateAsync(TaskCreateDTO task)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Task.Create, connection);
            command.CommandType = CommandType.StoredProcedure;
            AddCreateParameters(command, task);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TaskUpdateDTO task)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Task.Update, connection);
            command.CommandType = CommandType.StoredProcedure;
            AddUpdateParameters(command, task);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid taskId, string updatedBy)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Task.Delete, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(DbParameterExtensions.Create("@TaskId", taskId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy", updatedBy, SqlDbType.NVarChar));

            return await command.ExecuteNonQueryAsync() > 0;
        }
        #endregion

        #region Parameter Configuration
        private static void AddCreateParameters(SqlCommand command, TaskCreateDTO task)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@TaskId", task.TaskId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@FeatureId", task.FeatureId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@TechModuleId", task.TechModuleId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@Title", task.Title, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Description", task.Description, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", task.PriorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", task.StatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@EstimatedStartDate", task.EstimatedStartDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@EstimatedEndDate", task.EstimatedEndDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualStartDate", task.ActualStartDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualEndDate", task.ActualEndDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@LatestUpdate", task.LatestUpdate, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Notes", task.Notes, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@CreatedBy", task.CreatedBy, SqlDbType.NVarChar));
        }
        private static void AddUpdateParameters(SqlCommand command, TaskUpdateDTO task)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@TaskId", task.TaskId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@FeatureId", task.FeatureId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@TechModuleId", task.TechModuleId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@Title", task.Title, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Description", task.Description, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", task.PriorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", task.StatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@EstimatedStartDate", task.EstimatedStartDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@EstimatedEndDate", task.EstimatedEndDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualStartDate", task.ActualStartDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualEndDate", task.ActualEndDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@LatestUpdate", task.LatestUpdate, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Notes", task.Notes, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy", task.UpdatedBy, SqlDbType.NVarChar));
        }
        #endregion

        #region Mapping
        private static TaskDetailsDTO MapTask(SqlDataReader reader)
        {
            int descriptionOrdinal = reader.GetOrdinal("Description");
            int estimatedStartDateOrdinal = reader.GetOrdinal("EstimatedStartDate");
            int estimatedEndDateOrdinal = reader.GetOrdinal("EstimatedEndDate");
            int actualStartDateOrdinal = reader.GetOrdinal("ActualStartDate");
            int actualEndDateOrdinal = reader.GetOrdinal("ActualEndDate");
            int latestUpdateOrdinal = reader.GetOrdinal("LatestUpdate");
            int notesOrdinal = reader.GetOrdinal("Notes");
            int updatedByOrdinal = reader.GetOrdinal("UpdatedBy");
            int updatedDateTimeOrdinal = reader.GetOrdinal("UpdatedDateTime");

            return new TaskDetailsDTO
            {
                TaskId = reader.GetGuid(reader.GetOrdinal("TaskId")),
                FeatureId = reader.GetGuid(reader.GetOrdinal("FeatureId")),
                FeatureTitle = reader.GetString(reader.GetOrdinal("FeatureTitle")),
                TechModuleId = reader.GetGuid(reader.GetOrdinal("TechModuleId")),
                TechModule = reader.GetString(reader.GetOrdinal("TechModule")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Description = reader.IsDBNull(descriptionOrdinal) ? null : reader.GetString(descriptionOrdinal),
                PriorityId = reader.GetInt32(reader.GetOrdinal("PriorityId")),
                Priority = reader.GetString(reader.GetOrdinal("Priority")),
                StatusId = reader.GetInt32(reader.GetOrdinal("StatusId")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                EstimatedStartDate = reader.IsDBNull(estimatedStartDateOrdinal) ? null : reader.GetDateTime(estimatedStartDateOrdinal),
                EstimatedEndDate = reader.IsDBNull(estimatedEndDateOrdinal) ? null : reader.GetDateTime(estimatedEndDateOrdinal),
                ActualStartDate = reader.IsDBNull(actualStartDateOrdinal) ? null : reader.GetDateTime(actualStartDateOrdinal),
                ActualEndDate = reader.IsDBNull(actualEndDateOrdinal) ? null : reader.GetDateTime(actualEndDateOrdinal),
                LatestUpdate = reader.IsDBNull(latestUpdateOrdinal) ? null : reader.GetString(latestUpdateOrdinal),
                Notes = reader.IsDBNull(notesOrdinal) ? null : reader.GetString(notesOrdinal),
                CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                CreatedDateTime = reader.GetDateTime(reader.GetOrdinal("CreatedDateTime")),
                UpdatedBy = reader.IsDBNull(updatedByOrdinal) ? null : reader.GetString(updatedByOrdinal),
                UpdatedDateTime = reader.IsDBNull(updatedDateTimeOrdinal) ? null : reader.GetDateTime(updatedDateTimeOrdinal),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }
        #endregion
    }
}
