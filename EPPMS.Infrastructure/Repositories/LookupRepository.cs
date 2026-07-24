using EPPMS.Application.DTOs.Lookup;
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
    public sealed class LookupRepository : BaseRepository, ILookupRepository
    {
        public LookupRepository(ISqlConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
        #region Queries
        public Task<List<LookupDTO>> GetActionTypesAsync()
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.ActionType);
        public Task<List<LookupDTO>> GetComplexitiesAsync()
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.Complexity);
        public Task<List<LookupDTO>> GetCurrentHealthAsync()
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.CurrentHealth);
        public Task<List<LookupDTO>> GetPrioritiesAsync()
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.Priority);
        public Task<List<LookupDTO>> GetReleaseStatusesAsync()
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.ReleaseStatus);
        public Task<List<LookupDTO>> GetRequestTypesAsync()
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.RequestType);
        public Task<List<LookupDTO>> GetSeveritiesAsync()
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.Severity);
        public Task<List<LookupDTO>> GetStatusesAsync()
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.Status);
        public Task<List<LookupDTO>> GetTechnologyAreasAsync()
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.TechnologyArea);
        public Task<List<LookupDTO>> GetTestingStatusesAsync()
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.TestingStatus);

        public Task<List<LookupDTO>> GetTypesAsync()
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.Type);
        #endregion


        #region Commands

        #endregion

        #region Private Methods
        private async Task<List<LookupDTO>> ExecuteLookupAsync(string storedProcedure)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(storedProcedure,connection);
            command.CommandType = CommandType.StoredProcedure;
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            List<LookupDTO> lookups = [];
            while (await reader.ReadAsync())
            {
                lookups.Add(MapLookup(reader));
            }
            return lookups;
        }

        #endregion

        #region Mapping
        private static LookupDTO MapLookup(SqlDataReader reader)
        {
            return new LookupDTO
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                        ? null
                        : reader.GetString(
                            reader.GetOrdinal("Description")),
                SortOrder = reader.GetInt32(reader.GetOrdinal("SortOrder"))
            };
        }
        #endregion

    }
}
