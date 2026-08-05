using EPPMS.Application.DTOs.Error;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Services
{
    public interface IErrorPageService
    {
        Task<ErrorPageDTO> GetErrorPageAsync(int statusCode, CancellationToken cancellationToken = default);
    }
}