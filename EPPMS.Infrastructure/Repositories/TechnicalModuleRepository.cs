using EPPMS.Application.DTOs.TechnicalModule;
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
    public sealed class TechnicalModuleRepository : BaseRepository, ITechnicalModuleRepository
    {
        public TechnicalModuleRepository(ISqlConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        #region Queries

        public async Task<List<TechnicalModuleDetailsDTO>> GetTechnicalModulesAsync(
            Guid? techModuleId = null,
            int? technologyAreaId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.TechnicalModule.Get, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(DbParameterExtensions.Create("@TechModuleId", techModuleId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@TechnologyAreaId", technologyAreaId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", priorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", statusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@IsActive", isActive, SqlDbType.Bit));

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            List<TechnicalModuleDetailsDTO> technicalModules = [];

            while (await reader.ReadAsync())
            {
                technicalModules.Add(MapTechnicalModule(reader));
            }

            return technicalModules;
        }

        public async Task<TechnicalModuleDetailsDTO?> GetTechnicalModuleByIdAsync(Guid techModuleId)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.TechnicalModule.GetById, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(DbParameterExtensions.Create("@TechModuleId", techModuleId, SqlDbType.UniqueIdentifier));

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapTechnicalModule(reader);
            }

            return null;
        }
        #endregion

        #region Commands

        public async Task<bool> CreateAsync(TechnicalModuleCreateDTO technicalModule)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.TechnicalModule.Create, connection);
            command.CommandType = CommandType.StoredProcedure;
            AddCreateParameters(command, technicalModule);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TechnicalModuleUpdateDTO technicalModule)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.TechnicalModule.Update, connection);
            command.CommandType = CommandType.StoredProcedure;
            AddUpdateParameters(command, technicalModule);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid techModuleId, string updatedBy)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.TechnicalModule.Delete, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(DbParameterExtensions.Create("@TechModuleId", techModuleId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy", updatedBy, SqlDbType.NVarChar));
            return await command.ExecuteNonQueryAsync() > 0;
        }

        #endregion

        #region Parameter Configuration

        private static void AddCreateParameters(SqlCommand command, TechnicalModuleCreateDTO technicalModule)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@TechModuleId", technicalModule.TechModuleId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@TechModule", technicalModule.TechModule, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@TechnologyAreaId", technicalModule.TechnologyAreaId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@TaskTitle", technicalModule.TaskTitle, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Description", technicalModule.Description, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Reason", technicalModule.Reason, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", technicalModule.PriorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", technicalModule.StatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@PlannedStart", technicalModule.PlannedStart, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@TargetCompletion", technicalModule.TargetCompletion, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualStart", technicalModule.ActualStart, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualCompletion", technicalModule.ActualCompletion, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@EstimatedEffort", technicalModule.EstimatedEffort, SqlDbType.Decimal));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualEffort", technicalModule.ActualEffort, SqlDbType.Decimal));
            command.Parameters.Add(DbParameterExtensions.Create("@ReleaseImpact", technicalModule.ReleaseImpact, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@LatestUpdate", technicalModule.LatestUpdate, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Notes", technicalModule.Notes, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@CreatedBy", technicalModule.CreatedBy, SqlDbType.NVarChar));
        }

        private static void AddUpdateParameters(SqlCommand command, TechnicalModuleUpdateDTO technicalModule)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@TechModuleId", technicalModule.TechModuleId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@TechModule", technicalModule.TechModule, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@TechnologyAreaId", technicalModule.TechnologyAreaId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@TaskTitle", technicalModule.TaskTitle, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Description", technicalModule.Description, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Reason", technicalModule.Reason, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", technicalModule.PriorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", technicalModule.StatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@PlannedStart", technicalModule.PlannedStart, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@TargetCompletion", technicalModule.TargetCompletion, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualStart", technicalModule.ActualStart, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualCompletion", technicalModule.ActualCompletion, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@EstimatedEffort", technicalModule.EstimatedEffort, SqlDbType.Decimal));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualEffort", technicalModule.ActualEffort, SqlDbType.Decimal));
            command.Parameters.Add(DbParameterExtensions.Create("@ReleaseImpact", technicalModule.ReleaseImpact, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@LatestUpdate", technicalModule.LatestUpdate, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Notes", technicalModule.Notes, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy", technicalModule.UpdatedBy, SqlDbType.NVarChar));
        }
        #endregion

        #region Mapping

        private static TechnicalModuleDetailsDTO MapTechnicalModule(SqlDataReader reader)
        {
            int descriptionOrdinal = reader.GetOrdinal("Description");
            int reasonOrdinal = reader.GetOrdinal("Reason");
            int plannedStartOrdinal = reader.GetOrdinal("PlannedStart");
            int targetCompletionOrdinal = reader.GetOrdinal("TargetCompletion");
            int actualStartOrdinal = reader.GetOrdinal("ActualStart");
            int actualCompletionOrdinal = reader.GetOrdinal("ActualCompletion");
            int estimatedEffortOrdinal = reader.GetOrdinal("EstimatedEffort");
            int actualEffortOrdinal = reader.GetOrdinal("ActualEffort");
            int releaseImpactOrdinal = reader.GetOrdinal("ReleaseImpact");
            int latestUpdateOrdinal = reader.GetOrdinal("LatestUpdate");
            int notesOrdinal = reader.GetOrdinal("Notes");
            int updatedByOrdinal = reader.GetOrdinal("UpdatedBy");
            int updatedDateTimeOrdinal = reader.GetOrdinal("UpdatedDateTime");

            return new TechnicalModuleDetailsDTO
            {
                TechModuleId = reader.GetGuid(reader.GetOrdinal("TechModuleId")),
                TechModule = reader.GetString(reader.GetOrdinal("TechModule")),
                TechnologyAreaId = reader.GetInt32(reader.GetOrdinal("TechnologyAreaId")),
                TechnologyArea = reader.GetString(reader.GetOrdinal("TechnologyArea")),
                TaskTitle = reader.GetString(reader.GetOrdinal("TaskTitle")),
                Description = reader.IsDBNull(descriptionOrdinal) ? null : reader.GetString(descriptionOrdinal),
                Reason = reader.IsDBNull(reasonOrdinal) ? null : reader.GetString(reasonOrdinal),
                PriorityId = reader.GetInt32(reader.GetOrdinal("PriorityId")),
                Priority = reader.GetString(reader.GetOrdinal("Priority")),
                StatusId = reader.GetInt32(reader.GetOrdinal("StatusId")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                PlannedStart = reader.IsDBNull(plannedStartOrdinal) ? null : reader.GetDateTime(plannedStartOrdinal),
                TargetCompletion = reader.IsDBNull(targetCompletionOrdinal) ? null : reader.GetDateTime(targetCompletionOrdinal),
                ActualStart = reader.IsDBNull(actualStartOrdinal) ? null : reader.GetDateTime(actualStartOrdinal),
                ActualCompletion = reader.IsDBNull(actualCompletionOrdinal) ? null : reader.GetDateTime(actualCompletionOrdinal),
                EstimatedEffort = reader.IsDBNull(estimatedEffortOrdinal) ? null : reader.GetDecimal(estimatedEffortOrdinal),
                ActualEffort = reader.IsDBNull(actualEffortOrdinal) ? null : reader.GetDecimal(actualEffortOrdinal),
                ReleaseImpact = reader.IsDBNull(releaseImpactOrdinal) ? null : reader.GetString(releaseImpactOrdinal),
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
  
