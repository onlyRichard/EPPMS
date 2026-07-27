using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.User
{
    public class UserDetailsDTO
    {
        public string MSID { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public Guid AzureObjectId { get; set; }
        public string? SecurityGroupMemberships { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
