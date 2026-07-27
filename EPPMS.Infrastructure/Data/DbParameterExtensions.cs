using Microsoft.Data.SqlClient;
using System.Data;

namespace EPPMS.Infrastructure.Data;

public static class DbParameterExtensions
{
    public static SqlParameter Create(string parameterName,object? value, SqlDbType? dbType = null, ParameterDirection direction = ParameterDirection.Input)
    {
        SqlParameter parameter = new()
        {
            ParameterName = parameterName,
            Value = value ?? DBNull.Value,
            Direction = direction
        };
        if (dbType.HasValue)
        {
            parameter.SqlDbType = dbType.Value;
        }
        return parameter;
    }

    public static void AddParameter(this SqlParameterCollection parameters, string parameterName, object? value, SqlDbType dbType, ParameterDirection direction = ParameterDirection.Input)
    {
        parameters.Add(
            Create(
                parameterName,
                value,
                dbType,
                direction));
    }
}