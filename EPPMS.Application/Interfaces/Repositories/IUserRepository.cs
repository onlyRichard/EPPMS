using EPPMS.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> GetUsersAsync();
        Task<UserDetailsDTO?> GetUserByMSIDAsync(string msid);
        Task<bool> UpsertAsync(UserDTO user, DateTime lastLoginDateTime, string performedBy);
        Task<bool> UpdateAsync(UserUpdateDTO user);
        Task<bool> DeleteAsync(string msid);
    }
}
