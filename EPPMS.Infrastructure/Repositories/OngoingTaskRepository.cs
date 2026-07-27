using EPPMS.Application.DTOs.OngoingTask;
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
    public class OngoingTaskRepository : BaseRepository, IOngoingTaskRepository
    {
        public OngoingTaskRepository(ISqlConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

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
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.OngoingTask.Get, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(DbParameterExtensions.Create("@OngoingTaskId", ongoingTaskId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@AppId", appId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@TypeId", typeId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@SeverityId", severityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", priorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", statusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@IsActive", isActive, SqlDbType.Bit));

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            List<OngoingTaskDetailsDTO> ongoingTasks = [];

            while (await reader.ReadAsync())
            {
                ongoingTasks.Add(MapOngoingTask(reader));
            }

            return ongoingTasks;
        }

        public async Task<OngoingTaskDetailsDTO?> GetOngoingTaskByIdAsync(Guid ongoingTaskId)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.OngoingTask.GetById, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(DbParameterExtensions.Create("@OngoingTaskId", ongoingTaskId, SqlDbType.UniqueIdentifier));

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapOngoingTask(reader);
            }

            return null;
        }

        #endregion

        #region Commands

        public async Task<bool> CreateAsync(OngoingTaskCreateDTO ongoingTask)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.OngoingTask.Create, connection);
            command.CommandType = CommandType.StoredProcedure;

            AddCreateParameters(command, ongoingTask);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(OngoingTaskUpdateDTO ongoingTask)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.OngoingTask.Update, connection);
            command.CommandType = CommandType.StoredProcedure;

            AddUpdateParameters(command, ongoingTask);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid ongoingTaskId, string updatedBy)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.OngoingTask.Delete, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(DbParameterExtensions.Create("@OngoingTaskId", ongoingTaskId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy", updatedBy, SqlDbType.NVarChar));

            return await command.ExecuteNonQueryAsync() > 0;
        }
        #endregion
        #region Parameter Configuration

        private static void AddCreateParameters(SqlCommand command, OngoingTaskCreateDTO ongoingTask)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@OngoingTaskId", ongoingTask.OngoingTaskId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@AppId", ongoingTask.AppId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@TypeId", ongoingTask.TypeId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@Title", ongoingTask.Title, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Description", ongoingTask.Description, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@SeverityId", ongoingTask.SeverityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", ongoingTask.PriorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", ongoingTask.StatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StartDate", ongoingTask.StartDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@EndDate", ongoingTask.EndDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@CommentsUpdates", ongoingTask.CommentsUpdates, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@CreatedBy", ongoingTask.CreatedBy, SqlDbType.NVarChar));
        }

        private static void AddUpdateParameters(SqlCommand command, OngoingTaskUpdateDTO ongoingTask)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@OngoingTaskId", ongoingTask.OngoingTaskId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@AppId", ongoingTask.AppId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@TypeId", ongoingTask.TypeId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@Title", ongoingTask.Title, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Description", ongoingTask.Description, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@SeverityId", ongoingTask.SeverityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", ongoingTask.PriorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", ongoingTask.StatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StartDate", ongoingTask.StartDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@EndDate", ongoingTask.EndDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@CommentsUpdates", ongoingTask.CommentsUpdates, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy", ongoingTask.UpdatedBy, SqlDbType.NVarChar));
        }
        #endregion
        #region Mapping

        private static OngoingTaskDetailsDTO MapOngoingTask(SqlDataReader reader)
        {
            int descriptionOrdinal = reader.GetOrdinal("Description");
            int startDateOrdinal = reader.GetOrdinal("StartDate");
            int endDateOrdinal = reader.GetOrdinal("EndDate");
            int commentsUpdatesOrdinal = reader.GetOrdinal("CommentsUpdates");
            int updatedByOrdinal = reader.GetOrdinal("UpdatedBy");
            int updatedDateTimeOrdinal = reader.GetOrdinal("UpdatedDateTime");

            return new OngoingTaskDetailsDTO
            {
                OngoingTaskId = reader.GetGuid(reader.GetOrdinal("OngoingTaskId")),
                AppId = reader.GetGuid(reader.GetOrdinal("AppId")),
                ApplicationModule = reader.GetString(reader.GetOrdinal("ApplicationModule")),

                TypeId = reader.GetInt32(reader.GetOrdinal("TypeId")),
                Type = reader.GetString(reader.GetOrdinal("Type")),

                Title = reader.GetString(reader.GetOrdinal("Title")),
                Description = reader.IsDBNull(descriptionOrdinal)
                    ? null
                    : reader.GetString(descriptionOrdinal),

                SeverityId = reader.GetInt32(reader.GetOrdinal("SeverityId")),
                Severity = reader.GetString(reader.GetOrdinal("Severity")),

                PriorityId = reader.GetInt32(reader.GetOrdinal("PriorityId")),
                Priority = reader.GetString(reader.GetOrdinal("Priority")),

                StatusId = reader.GetInt32(reader.GetOrdinal("StatusId")),
                Status = reader.GetString(reader.GetOrdinal("Status")),

                StartDate = reader.IsDBNull(startDateOrdinal)
                    ? null
                    : reader.GetDateTime(startDateOrdinal),

                EndDate = reader.IsDBNull(endDateOrdinal)
                    ? null
                    : reader.GetDateTime(endDateOrdinal),

                CommentsUpdates = reader.IsDBNull(commentsUpdatesOrdinal)
                    ? null
                    : reader.GetString(commentsUpdatesOrdinal),

                CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                CreatedDateTime = reader.GetDateTime(reader.GetOrdinal("CreatedDateTime")),

                UpdatedBy = reader.IsDBNull(updatedByOrdinal)
                    ? null
                    : reader.GetString(updatedByOrdinal),

                UpdatedDateTime = reader.IsDBNull(updatedDateTimeOrdinal)
                    ? null
                    : reader.GetDateTime(updatedDateTimeOrdinal),

                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }
        #endregion
    }
}
  
