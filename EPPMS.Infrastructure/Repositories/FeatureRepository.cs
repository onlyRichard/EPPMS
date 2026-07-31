using EPPMS.Application.DTOs.Feature;
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

    public sealed class FeatureRepository : BaseRepository, IFeatureRepository
    {
        public FeatureRepository(ISqlConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        #region Queries

        public async Task<List<FeatureDetailsDTO>> GetFeaturesAsync(string? search = null,Guid? appId = null,int? priorityId = null,int? statusId = null,int? requestTypeId = null,bool isActive = true)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Feature.Get, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(DbParameterExtensions.Create("@Search", search, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@AppId", appId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", priorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", statusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@RequestTypeId", requestTypeId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@IsActive", isActive, SqlDbType.Bit));
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            List<FeatureDetailsDTO> features = [];
            while (await reader.ReadAsync())
            {
                features.Add(MapFeature(reader));
            }
            return features;
        }

        public async Task<FeatureDetailsDTO?> GetFeatureByIdAsync(Guid featureId)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Feature.GetById, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(DbParameterExtensions.Create("@FeatureId", featureId, SqlDbType.UniqueIdentifier));

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapFeature(reader);
            }

            return null;
        }
        #endregion

        #region Commands
        public async Task<bool> CreateAsync(FeatureCreateDTO feature)
        {
            feature.CreatedBy = "MS/rperal15";
            feature.FeatureId = Guid.NewGuid();
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Feature.Create, connection);
            command.CommandType = CommandType.StoredProcedure;
            AddCreateParameters(command, feature);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(FeatureUpdateDTO feature)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Feature.Update, connection);
            command.CommandType = CommandType.StoredProcedure;
            AddUpdateParameters(command, feature);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid featureId, string updatedBy)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Feature.Delete, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(DbParameterExtensions.Create("@FeatureId", featureId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy", updatedBy, SqlDbType.NVarChar));
            return await command.ExecuteNonQueryAsync() > 0;
        }

        #endregion

        #region Private Methods
        private static void AddCreateParameters(SqlCommand command, FeatureCreateDTO feature)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@FeatureId", feature.FeatureId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@AppId", feature.AppId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@Title", feature.Title, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@RequestTypeId", feature.RequestTypeId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@Description", feature.Description, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@RequestedBy", feature.RequestedBy, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@DateRaised", feature.DateRaised, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@BusinessNeed", feature.BusinessNeed, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@ExpectedValue", feature.ExpectedValue, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", feature.PriorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@UsersImpacted", feature.UsersImpacted, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@InitialTechAssessment", feature.InitialTechAssessment, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@ComplexityId", feature.ComplexityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@ApproxEffort", feature.ApproxEffort, SqlDbType.Decimal));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", feature.StatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@TargetRelease", feature.TargetRelease, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@LinksNotes", feature.LinksNotes, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@CreatedBy", feature.CreatedBy, SqlDbType.NVarChar));
        }

        private static void AddUpdateParameters(SqlCommand command, FeatureUpdateDTO feature)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@FeatureId", feature.FeatureId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@AppId", feature.AppId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@Title", feature.Title, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@RequestTypeId", feature.RequestTypeId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@Description", feature.Description, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@RequestedBy", feature.RequestedBy, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@DateRaised", feature.DateRaised, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@BusinessNeed", feature.BusinessNeed, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@ExpectedValue", feature.ExpectedValue, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@PriorityId", feature.PriorityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@UsersImpacted", feature.UsersImpacted, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@InitialTechAssessment", feature.InitialTechAssessment, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@ComplexityId", feature.ComplexityId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@ApproxEffort", feature.ApproxEffort, SqlDbType.Decimal));
            command.Parameters.Add(DbParameterExtensions.Create("@StatusId", feature.StatusId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@TargetRelease", feature.TargetRelease, SqlDbType.Date));
            command.Parameters.Add(DbParameterExtensions.Create("@LinksNotes", feature.LinksNotes, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy", feature.UpdatedBy, SqlDbType.NVarChar));
        }
        #endregion

        #region Mapping
        private static FeatureDetailsDTO MapFeature(SqlDataReader reader)
        {
            int linksNotesOrdinal = reader.GetOrdinal("LinksNotes");
            int updatedByOrdinal = reader.GetOrdinal("UpdatedBy");
            int updatedDateTimeOrdinal = reader.GetOrdinal("UpdatedDateTime");
            int targetReleaseOrdinal = reader.GetOrdinal("TargetRelease");

            return new FeatureDetailsDTO
            {
                FeatureId = reader.GetGuid(reader.GetOrdinal("FeatureId")),
                AppId = reader.GetGuid(reader.GetOrdinal("AppId")),
                ApplicationModule = reader.GetString(reader.GetOrdinal("ApplicationModule")),
                Title = reader.GetString(reader.GetOrdinal("Title")),

                RequestTypeId = reader.GetInt32(reader.GetOrdinal("RequestTypeId")),
                RequestType = reader.GetString(reader.GetOrdinal("RequestType")),

                Description = reader.GetString(reader.GetOrdinal("Description")),
                RequestedBy = reader.GetString(reader.GetOrdinal("RequestedBy")),
                DateRaised = reader.GetDateTime(reader.GetOrdinal("DateRaised")),

                BusinessNeed = reader.GetString(reader.GetOrdinal("BusinessNeed")),
                ExpectedValue = reader.GetString(reader.GetOrdinal("ExpectedValue")),

                PriorityId = reader.GetInt32(reader.GetOrdinal("PriorityId")),
                Priority = reader.GetString(reader.GetOrdinal("Priority")),

                UsersImpacted = reader.GetInt32(reader.GetOrdinal("UsersImpacted")),

                InitialTechAssessment = reader.GetString(reader.GetOrdinal("InitialTechAssessment")),

                ComplexityId = reader.GetInt32(reader.GetOrdinal("ComplexityId")),
                Complexity = reader.GetString(reader.GetOrdinal("Complexity")),

                ApproxEffort = reader.GetDecimal(reader.GetOrdinal("ApproxEffort")),

                StatusId = reader.GetInt32(reader.GetOrdinal("StatusId")),
                Status = reader.GetString(reader.GetOrdinal("Status")),

                TargetRelease = reader.IsDBNull(targetReleaseOrdinal)
                    ? null
                    : reader.GetDateTime(targetReleaseOrdinal),

                LinksNotes = reader.IsDBNull(linksNotesOrdinal)
                    ? null
                    : reader.GetString(linksNotesOrdinal),

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
