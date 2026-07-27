using EPPMS.Application.DTOs.User;

namespace EPPMS.Application.Interfaces.Services
{
    public interface IUserService
    {
        #region Queries
        Task<List<UserDTO>> GetUsersAsync();
        Task<UserDetailsDTO> GetUserDetailsAsync(string msid);
        #endregion

        #region Commands
        Task<bool> SynchronizeUserAsync(
            UserDTO user,
            DateTime lastLoginDateTime,
            string performedBy);

        Task<bool> UpdateUserAsync(UserUpdateDTO user);
        Task<bool> DeleteUserAsync(string msid);
        #endregion
    }
}