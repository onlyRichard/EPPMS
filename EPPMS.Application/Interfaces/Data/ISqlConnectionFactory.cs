using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Data
{
    public interface ISqlConnectionFactory
    {
        Task<SqlConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);
    }
}
