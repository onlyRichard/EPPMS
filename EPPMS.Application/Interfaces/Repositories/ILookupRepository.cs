using EPPMS.Application.DTOs.Lookup;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Repositories
{
    public interface ILookupRepository
    {
        Task<List<LookupDTO>> GetActionTypesAsync();
        Task<List<LookupDTO>> GetComplexitiesAsync();
        Task<List<LookupDTO>> GetCurrentHealthAsync();
        Task<List<LookupDTO>> GetPrioritiesAsync();
        Task<List<LookupDTO>> GetReleaseStatusesAsync();
        Task<List<LookupDTO>> GetRequestTypesAsync();
        Task<List<LookupDTO>> GetSeveritiesAsync();
        Task<List<LookupDTO>> GetStatusesAsync();
        Task<List<LookupDTO>> GetTechnologyAreasAsync();
        Task<List<LookupDTO>> GetTestingStatusesAsync();
        Task<List<LookupDTO>> GetTypesAsync();
    }
}
