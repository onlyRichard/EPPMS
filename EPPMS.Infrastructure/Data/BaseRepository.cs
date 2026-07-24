using EPPMS.Application.Interfaces.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Infrastructure.Data
{
    public abstract class BaseRepository    {
        protected readonly ISqlConnectionFactory ConnectionFactory;
        protected BaseRepository(ISqlConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory
                ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        protected Task<SqlConnection> CreateConnectionAsync(
            CancellationToken cancellationToken = default)
        {
            return ConnectionFactory.CreateConnectionAsync(cancellationToken);
        }
    }
}
