using EPPMS.Application.DTOs.User;
using EPPMS.Application.Interfaces.Data;
using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EPPMS.Infrastructure.Repositories
{
    public sealed class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ISqlConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        #region Queries
        public async Task<List<UserDTO>> GetUsersAsync()
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.User.Get, connection);
            command.CommandType = CommandType.StoredProcedure;
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            List<UserDTO> users = [];
            while (await reader.ReadAsync())
            {
                users.Add(MapUser(reader));
            }
            return users;
        }

        public async Task<UserDetailsDTO?> GetUserByMSIDAsync(string msid)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.User.GetByMSID, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(DbParameterExtensions.Create("@MSID",msid,SqlDbType.NVarChar));
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapUserDetails(reader);
            }
            return null;
        }

        #endregion

        #region Commands
        public async Task<bool> UpsertAsync(UserDTO user,DateTime lastLoginDateTime,string performedBy)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.User.Upsert, connection);
            command.CommandType = CommandType.StoredProcedure;
            AddUpsertParameters(command,user,lastLoginDateTime,performedBy);
            return await command.ExecuteNonQueryAsync() > 0;
        }
        public async Task<bool> UpdateAsync(UserUpdateDTO user)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.User.Update, connection);
            command.CommandType = CommandType.StoredProcedure;
            AddUpdateParameters(command, user);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string msid)
        {
            await using SqlConnection connection = await CreateConnectionAsync();
            await using SqlCommand command = new(StoredProcedureNames.User.Delete,   connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(DbParameterExtensions.Create("@MSID",msid, SqlDbType.NVarChar));
            return await command.ExecuteNonQueryAsync() > 0;
        }

        #endregion

        #region Private Methods

        private static void AddUpsertParameters(SqlCommand command,UserDTO user,DateTime lastLoginDateTime,string performedBy)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@MSID",user.MSID,SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@DisplayName",user.DisplayName, SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@EmailAddress",user.EmailAddress,SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@AzureObjectId",user.AzureObjectId,SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@SecurityGroupMemberships",user.SecurityGroupMemberships,SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@LastLoginDateTime",lastLoginDateTime,SqlDbType.DateTime2));
            command.Parameters.Add(DbParameterExtensions.Create("@PerformedBy",performedBy,SqlDbType.NVarChar));
        }

        private static void AddUpdateParameters(SqlCommand command,UserUpdateDTO user)
        {
            command.Parameters.Add(DbParameterExtensions.Create("@MSID",user.MSID,SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@DisplayName",user.DisplayName,SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@EmailAddress",user.EmailAddress,SqlDbType.NVarChar));
            command.Parameters.Add(DbParameterExtensions.Create("@AzureObjectId", user.AzureObjectId,SqlDbType.UniqueIdentifier));
            command.Parameters.Add(DbParameterExtensions.Create("@SecurityGroupMemberships",user.SecurityGroupMemberships, SqlDbType.NVarChar));
        }

        #endregion

        #region Mapping

        private static UserDTO MapUser(SqlDataReader reader)
        {
            int securityGroupMembershipsOrdinal =  reader.GetOrdinal("SecurityGroupMemberships");
            return new UserDTO
            {
                MSID = reader.GetString(reader.GetOrdinal("MSID")),
                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                AzureObjectId = reader.GetGuid(reader.GetOrdinal("AzureObjectId")),
                SecurityGroupMemberships = reader.IsDBNull(securityGroupMembershipsOrdinal)
                        ? null
                        : reader.GetString(securityGroupMembershipsOrdinal)
            };
        }

        private static UserDetailsDTO MapUserDetails(SqlDataReader reader)
        {
            int securityGroupMembershipsOrdinal = reader.GetOrdinal("SecurityGroupMemberships");
            int lastLoginDateTimeOrdinal = reader.GetOrdinal("LastLoginDateTime");
            int createdByOrdinal = reader.GetOrdinal("CreatedBy");
            int updatedByOrdinal = reader.GetOrdinal("UpdatedBy");
            int updatedDateTimeOrdinal = reader.GetOrdinal("UpdatedDateTime");
            return new UserDetailsDTO
            {
                MSID = reader.GetString(reader.GetOrdinal("MSID")),
                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                AzureObjectId = reader.GetGuid(reader.GetOrdinal("AzureObjectId")),
                SecurityGroupMemberships = reader.IsDBNull(securityGroupMembershipsOrdinal)
                        ? null
                        : reader.GetString(securityGroupMembershipsOrdinal),
                LastLoginDateTime = reader.IsDBNull(lastLoginDateTimeOrdinal)
                        ? null
                        : reader.GetDateTime(lastLoginDateTimeOrdinal),
                CreatedBy = reader.IsDBNull(createdByOrdinal)
                        ? null
                        : reader.GetString(createdByOrdinal),
                CreatedDateTime = reader.GetDateTime(reader.GetOrdinal("CreatedDateTime")),
                UpdatedBy = reader.IsDBNull(updatedByOrdinal)
                        ? null
                        : reader.GetString(updatedByOrdinal),
                UpdatedDateTime = reader.IsDBNull(updatedDateTimeOrdinal)
                        ? null
                        : reader.GetDateTime(updatedDateTimeOrdinal),
                IsActive = reader.GetBoolean(
                    reader.GetOrdinal("IsActive"))
            };
        }

        #endregion
    }
}
