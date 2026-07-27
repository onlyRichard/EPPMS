using EPPMS.Application.DTOs.Bug;
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
    public class BugRepository : BaseRepository, IBugRepository
    {
        public BugRepository(ISqlConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        #region Queries

        public async Task<List<BugDetailsDTO>> GetBugsAsync(
            Guid? bugId = null,
            Guid? appId = null,
            int? severityId = null,
            int? priorityId = null,
            int? statusId = null,
            bool isActive = true)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Bug.Get, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(DbParameterExtensions.Create("@BugId", bugId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@AppId", appId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@SeverityId", severityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", priorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", statusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@IsActive", isActive, SqlDbType.Bit));

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            List<BugDetailsDTO> bugs = [];

            while (await reader.ReadAsync())
            {
                bugs.Add(MapBug(reader));
            }

            return bugs;
        }

        public async Task<BugDetailsDTO?> GetBugByIdAsync(Guid bugId)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Bug.GetById, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(DbParameterExtensions.Create("@BugId", bugId, SqlDbType.UniqueIdentifier));

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapBug(reader);
            }

            return null;
        }

        #endregion

        #region Commands

        public async Task<bool> CreateAsync(BugCreateDTO bug)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Bug.Create, connection);
            command.CommandType = CommandType.StoredProcedure;
            AddCreateParameters(command, bug);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BugUpdateDTO bug)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Bug.Update, connection);
            command.CommandType = CommandType.StoredProcedure;
            AddUpdateParameters(command, bug);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid bugId, string updatedBy)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Bug.Delete, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(DbParameterExtensions.Create("@BugId", bugId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy", updatedBy, SqlDbType.NVarChar));

            return await command.ExecuteNonQueryAsync() > 0;
        }
        #endregion
        #region Parameter Configuration

        private static void AddCreateParameters(SqlCommand command, BugCreateDTO bug)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@BugId", bug.BugId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@AppId", bug.AppId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@BugTitle", bug.BugTitle, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Description", bug.Description, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@NumberOfUsersImpacted", bug.NumberOfUsersImpacted, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@ReportedBy", bug.ReportedBy, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@ReportedDate", bug.ReportedDate, SqlDbType.DateTime2));
            command.Parameters.Add(DbParameterExtensions.Create("@SeverityId", bug.SeverityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", bug.PriorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@UserBusinessImpact", bug.UserBusinessImpact, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@ReproductionSteps", bug.ReproductionSteps, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@RootCause", bug.RootCause, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@AssignedTo", bug.AssignedTo, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", bug.StatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@WorkaroundAvailable", bug.WorkaroundAvailable, SqlDbType.Bit));
            command.Parameters.Add(DbParameterExtensions.Create("@StartDate", bug.StartDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@EndDate", bug.EndDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ReleaseDate", bug.ReleaseDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ReleaseStatusId", bug.ReleaseStatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@EstimatedEffort", bug.EstimatedEffort, SqlDbType.Decimal));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualEffort", bug.ActualEffort, SqlDbType.Decimal));
            command.Parameters.Add(DbParameterExtensions.Create("@TestingStatusId", bug.TestingStatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@ProductionDeploymentDate", bug.ProductionDeploymentDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@CommentsUpdates", bug.CommentsUpdates, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@CreatedBy", bug.CreatedBy, SqlDbType.NVarChar));
        }

        private static void AddUpdateParameters(SqlCommand command, BugUpdateDTO bug)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@BugId", bug.BugId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@AppId", bug.AppId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@BugTitle", bug.BugTitle, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Description", bug.Description, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@NumberOfUsersImpacted", bug.NumberOfUsersImpacted, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@ReportedBy", bug.ReportedBy, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@ReportedDate", bug.ReportedDate, SqlDbType.DateTime2));
            command.Parameters.Add(DbParameterExtensions.Create("@SeverityId", bug.SeverityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", bug.PriorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@UserBusinessImpact", bug.UserBusinessImpact, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@ReproductionSteps", bug.ReproductionSteps, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@RootCause", bug.RootCause, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@AssignedTo", bug.AssignedTo, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", bug.StatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@WorkaroundAvailable", bug.WorkaroundAvailable, SqlDbType.Bit));
            command.Parameters.Add(DbParameterExtensions.Create("@StartDate", bug.StartDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@EndDate", bug.EndDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ReleaseDate", bug.ReleaseDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@ReleaseStatusId", bug.ReleaseStatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@EstimatedEffort", bug.EstimatedEffort, SqlDbType.Decimal));
            command.Parameters.Add(DbParameterExtensions.Create("@ActualEffort", bug.ActualEffort, SqlDbType.Decimal));
            command.Parameters.Add(DbParameterExtensions.Create("@TestingStatusId", bug.TestingStatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@ProductionDeploymentDate", bug.ProductionDeploymentDate, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@CommentsUpdates", bug.CommentsUpdates, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy", bug.UpdatedBy, SqlDbType.NVarChar));
        }
        #endregion
        #region Mapping

        private static BugDetailsDTO MapBug(SqlDataReader reader)
        {
            int descriptionOrdinal = reader.GetOrdinal("Description");
            int numberOfUsersImpactedOrdinal = reader.GetOrdinal("NumberOfUsersImpacted");
            int userBusinessImpactOrdinal = reader.GetOrdinal("UserBusinessImpact");
            int reproductionStepsOrdinal = reader.GetOrdinal("ReproductionSteps");
            int rootCauseOrdinal = reader.GetOrdinal("RootCause");
            int assignedToOrdinal = reader.GetOrdinal("AssignedTo");
            int startDateOrdinal = reader.GetOrdinal("StartDate");
            int endDateOrdinal = reader.GetOrdinal("EndDate");
            int releaseDateOrdinal = reader.GetOrdinal("ReleaseDate");
            int estimatedEffortOrdinal = reader.GetOrdinal("EstimatedEffort");
            int actualEffortOrdinal = reader.GetOrdinal("ActualEffort");
            int productionDeploymentDateOrdinal = reader.GetOrdinal("ProductionDeploymentDate");
            int commentsUpdatesOrdinal = reader.GetOrdinal("CommentsUpdates");
            int updatedByOrdinal = reader.GetOrdinal("UpdatedBy");
            int updatedDateTimeOrdinal = reader.GetOrdinal("UpdatedDateTime");

            return new BugDetailsDTO
            {
                BugId = reader.GetGuid(reader.GetOrdinal("BugId")),
                AppId = reader.GetGuid(reader.GetOrdinal("AppId")),
                ApplicationModule = reader.GetString(reader.GetOrdinal("ApplicationModule")),
                BugTitle = reader.GetString(reader.GetOrdinal("BugTitle")),
                Description = reader.IsDBNull(descriptionOrdinal) ? null : reader.GetString(descriptionOrdinal),
                NumberOfUsersImpacted = reader.IsDBNull(numberOfUsersImpactedOrdinal) ? null : reader.GetInt32(numberOfUsersImpactedOrdinal),
                ReportedBy = reader.GetString(reader.GetOrdinal("ReportedBy")),
                ReportedDate = reader.GetDateTime(reader.GetOrdinal("ReportedDate")),
                SeverityId = reader.GetInt32(reader.GetOrdinal("SeverityId")),
                Severity = reader.GetString(reader.GetOrdinal("Severity")),
                PriorityId = reader.GetInt32(reader.GetOrdinal("PriorityId")),
                Priority = reader.GetString(reader.GetOrdinal("Priority")),
                UserBusinessImpact = reader.IsDBNull(userBusinessImpactOrdinal) ? null : reader.GetString(userBusinessImpactOrdinal),
                ReproductionSteps = reader.IsDBNull(reproductionStepsOrdinal) ? null : reader.GetString(reproductionStepsOrdinal),
                RootCause = reader.IsDBNull(rootCauseOrdinal) ? null : reader.GetString(rootCauseOrdinal),
                AssignedTo = reader.IsDBNull(assignedToOrdinal) ? null : reader.GetString(assignedToOrdinal),
                StatusId = reader.GetInt32(reader.GetOrdinal("StatusId")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                WorkaroundAvailable = reader.GetBoolean(reader.GetOrdinal("WorkaroundAvailable")),
                StartDate = reader.IsDBNull(startDateOrdinal) ? null : reader.GetDateTime(startDateOrdinal),
                EndDate = reader.IsDBNull(endDateOrdinal) ? null : reader.GetDateTime(endDateOrdinal),
                ReleaseDate = reader.IsDBNull(releaseDateOrdinal) ? null : reader.GetDateTime(releaseDateOrdinal),
                ReleaseStatusId = reader.GetInt32(reader.GetOrdinal("ReleaseStatusId")),
                ReleaseStatus = reader.GetString(reader.GetOrdinal("ReleaseStatus")),
                EstimatedEffort = reader.IsDBNull(estimatedEffortOrdinal) ? null : reader.GetDecimal(estimatedEffortOrdinal),
                ActualEffort = reader.IsDBNull(actualEffortOrdinal) ? null : reader.GetDecimal(actualEffortOrdinal),
                TestingStatusId = reader.GetInt32(reader.GetOrdinal("TestingStatusId")),
                TestingStatus = reader.GetString(reader.GetOrdinal("TestingStatus")),
                ProductionDeploymentDate = reader.IsDBNull(productionDeploymentDateOrdinal) ? null : reader.GetDateTime(productionDeploymentDateOrdinal),
                CommentsUpdates = reader.IsDBNull(commentsUpdatesOrdinal) ? null : reader.GetString(commentsUpdatesOrdinal),
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
 