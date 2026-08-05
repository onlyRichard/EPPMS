using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.Error
{
    public sealed class ErrorPageDTO
    {      
        public string ErrorReference { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool ShowGoBack { get; set; } = true;
        public bool ShowDashboardButton { get; set; } = true;
        public string DashboardUrl { get; set; } = "/";
        public string SupportContact { get; set; }
            = "EPPMS Administrator";
    }
}
