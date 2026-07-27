using EPPMS.Application.DTOs.Application;
using EPPMS.Application.DTOs.Application.Requests;
using EPPMS.Application.DTOs.Application.Response;
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
    public sealed class ApplicationRepository : BaseRepository, IApplicationRepository
    {
        public ApplicationRepository(ISqlConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        #region Queries
        public async Task<List<ApplicationListResponseDTO>> GetApplicationsAsync(string? search = null,int? currentHealthId = null, bool isActive = true)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Application.Get,connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(DbParameterExtensions.Create("@Search",search,SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@CurrentHealthId",currentHealthId,SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@IsActive",isActive,SqlDbType.Bit));
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            List<ApplicationListResponseDTO> applications = [];
            while (await reader.ReadAsync())
            {
                applications.Add(MapApplication(reader));
            }
            return applications;
        }

        public async Task<ApplicationListResponseDTO?> GetApplicationByIdAsync(Guid appId)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Application.GetById,connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(DbParameterExtensions.Create("@AppId",appId,SqlDbType.UniqueIdentifier));
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapApplication(reader);
            }
            return null;
        }

        #endregion

        #region Commands
        public async Task<bool> CreateAsync(ApplicationCreateDTO application)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Application.Create,connection);
            command.CommandType = CommandType.StoredProcedure;
            AddCreateParameters(command, application);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ApplicationUpdateDTO application)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Application.Update,connection);
            command.CommandType = CommandType.StoredProcedure;
            AddUpdateParameters(command, application);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid appId, string updatedBy)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.Application.Delete,connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(DbParameterExtensions.Create("@AppId",appId,SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy",updatedBy,SqlDbType.NVarChar));
            return await command.ExecuteNonQueryAsync() > 0;
        }

        #endregion

        #region Parameter Configuration
        private static void AddCreateParameters(SqlCommand command,ApplicationCreateDTO application)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@AppId", application.AppId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@ApplicationModule", application.ApplicationModule, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Purpose", application.Purpose, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@TechDetails", application.TechDetails, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@ProductionUrl", application.ProductionUrl, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@CurrentHealthId", application.CurrentHealthId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@Notes", application.Notes, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@CreatedBy", application.CreatedBy, SqlDbType.NVarChar));
        }

        private static void AddUpdateParameters(SqlCommand command,ApplicationUpdateDTO application)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@AppId", application.AppId, SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@ApplicationModule", application.ApplicationModule, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@Purpose", application.Purpose, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@TechDetails", application.TechDetails, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@ProductionUrl", application.ProductionUrl, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@CurrentHealthId", application.CurrentHealthId, SqlDbType.Int));
            command.Parameters.Add(DbParameterExtensions.Create("@Notes", application.Notes, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@UpdatedBy", application.UpdatedBy, SqlDbType.NVarChar));
        }

        #endregion

        #region Mapping
        private static ApplicationListResponseDTO MapApplication(SqlDataReader reader)
        {
            int notesOrdinal = reader.GetOrdinal("Notes");
            int updatedByOrdinal = reader.GetOrdinal("UpdatedBy");
            int updatedDateTimeOrdinal = reader.GetOrdinal("UpdatedDateTime");

            return new ApplicationListResponseDTO
            {
                AppId = reader.GetGuid(reader.GetOrdinal("AppId")),
                ApplicationModule = reader.GetString(reader.GetOrdinal("ApplicationModule")),
                Purpose = reader.GetString(reader.GetOrdinal("Purpose")),
                TechDetails = reader.GetString(reader.GetOrdinal("TechDetails")),
                ProductionUrl = reader.GetString(reader.GetOrdinal("ProductionUrl")),
                CurrentHealthId = reader.GetInt32(reader.GetOrdinal("CurrentHealthId")),
                CurrentHealth = reader.GetString(reader.GetOrdinal("CurrentHealth")),
                Notes = reader.IsDBNull(notesOrdinal)
                    ? null
                    : reader.GetString(notesOrdinal),
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
