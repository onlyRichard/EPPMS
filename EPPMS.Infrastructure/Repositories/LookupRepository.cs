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

        public Task<IReadOnlyList<LookupResponseDTO>> GetActionTypesAsync(
            CancellationToken cancellationToken)
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.ActionType, cancellationToken);

        public Task<IReadOnlyList<LookupResponseDTO>> GetComplexitiesAsync(
            CancellationToken cancellationToken)
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.Complexity, cancellationToken);
        public Task<IReadOnlyList<LookupResponseDTO>> GetCurrentHealthsAsync(
            CancellationToken cancellationToken)
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.CurrentHealth, cancellationToken);
        public Task<IReadOnlyList<LookupResponseDTO>> GetPrioritiesAsync(
            CancellationToken cancellationToken)
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.Priority, cancellationToken);
        public Task<IReadOnlyList<LookupResponseDTO>> GetReleaseStatusesAsync(
            CancellationToken cancellationToken)
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.ReleaseStatus, cancellationToken);
        public Task<IReadOnlyList<LookupResponseDTO>> GetRequestTypesAsync(
            CancellationToken cancellationToken)
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.RequestType, cancellationToken);
        public Task<IReadOnlyList<LookupResponseDTO>> GetSeveritiesAsync(
            CancellationToken cancellationToken)
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.Severity, cancellationToken);
        public Task<IReadOnlyList<LookupResponseDTO>> GetStatusesAsync(
            CancellationToken cancellationToken)
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.Status, cancellationToken);
        public Task<IReadOnlyList<LookupResponseDTO>> GetTechnologyAreasAsync(
            CancellationToken cancellationToken)
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.TechnologyArea, cancellationToken);
        public Task<IReadOnlyList<LookupResponseDTO>> GetTestingStatusesAsync(
            CancellationToken cancellationToken)
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.TestingStatus, cancellationToken);
        public Task<IReadOnlyList<LookupResponseDTO>> GetTypesAsync(
            CancellationToken cancellationToken)
            => ExecuteLookupAsync(StoredProcedureNames.Lookup.Type,cancellationToken);

        public Task<IReadOnlyList<ModulesLookupResponseDTO>> GetApplicationAsync(
            CancellationToken cancellationToken)
            => ExecuteApplicationsLookupAsync(StoredProcedureNames.Lookup.Application, cancellationToken);

        public Task<IReadOnlyList<ModulesLookupResponseDTO>> GetFeatureAsync(
          CancellationToken cancellationToken)
          => ExecuteApplicationsLookupAsync(StoredProcedureNames.Lookup.Feature, cancellationToken);

        public Task<IReadOnlyList<ModulesLookupResponseDTO>> GetTechnicalModuleAsync(
         CancellationToken cancellationToken)
         => ExecuteApplicationsLookupAsync(StoredProcedureNames.Lookup.TechnicalModule, cancellationToken);

        public Task<IReadOnlyList<ModulesLookupResponseDTO>> GetBugAsync(
         CancellationToken cancellationToken)
         => ExecuteApplicationsLookupAsync(StoredProcedureNames.Lookup.Bug, cancellationToken);
        #endregion


        #region Commands

        #endregion

        #region Private Methods
        private async Task<IReadOnlyList<LookupResponseDTO>> ExecuteLookupAsync(string storedProcedure,CancellationToken cancellationToken)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(storedProcedure, connection);

            command.CommandType = CommandType.StoredProcedure;
            await using SqlDataReader reader =  await command.ExecuteReaderAsync(cancellationToken);

            List<LookupResponseDTO> lookups = [];
            while (await reader.ReadAsync(cancellationToken))
            {
                lookups.Add(MapLookup(reader));
            }

            return lookups;
        }

        private async Task<IReadOnlyList<ModulesLookupResponseDTO>> ExecuteApplicationsLookupAsync(string storedProcedure, CancellationToken cancellationToken)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(storedProcedure, connection);

            command.CommandType = CommandType.StoredProcedure;
            await using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

            List<ModulesLookupResponseDTO> lookups = [];
            while (await reader.ReadAsync(cancellationToken))
            {
                lookups.Add(MapModulesLookup(reader));
            }

            return lookups;
        }
        #endregion

        #region Mapping
        private static LookupResponseDTO MapLookup(SqlDataReader reader)
        {
            return new LookupResponseDTO
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

        private static ModulesLookupResponseDTO MapModulesLookup(SqlDataReader reader)
        {
            return new ModulesLookupResponseDTO
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name"))
            };
        }

      
        #endregion

    }
}
