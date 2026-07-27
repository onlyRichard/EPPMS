using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DTOs.User
{
    public class UserUpdateDTO
    {
        public string MSID { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public Guid AzureObjectId { get; set; }
        public string? SecurityGroupMemberships { get; set; }
    }
}
