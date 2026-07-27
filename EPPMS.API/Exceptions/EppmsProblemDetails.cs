using Microsoft.AspNetCore.Mvc;

namespace EPPMS.API.Exceptions
{
    public sealed class EppmsProblemDetails : ProblemDetails
    {
        #region Constructor

        public EppmsProblemDetails()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }

        #endregion

        #region Properties
        public string? TraceId { get; init; }
        public DateTimeOffset Timestamp { get; }
        public IReadOnlyDictionary<string, string[]>? Errors { get; init; }
        #endregion
    }
}