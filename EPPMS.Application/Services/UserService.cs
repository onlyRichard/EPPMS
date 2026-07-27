using EPPMS.Application.DTOs.User;
using EPPMS.Application.Exceptions;
using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Application.Interfaces.Services;

namespace EPPMS.Application.Services
{
    public sealed class UserService : IUserService
    {
        #region Fields
        private readonly IUserRepository _userRepository;
        #endregion

        #region Constructor
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Queries
        public async Task<List<UserDTO>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        public async Task<UserDetailsDTO> GetUserDetailsAsync(string msid)
        {
            var user = await _userRepository.GetUserByMSIDAsync(msid);

            if (user is null)
            {
                throw new NotFoundException($"User '{msid}' was not found.");
            }
            return user;
        }
        #endregion

        #region Commands

        public async Task<bool> SynchronizeUserAsync(UserDTO user, DateTime lastLoginDateTime, string performedBy)
        {
            return await _userRepository.UpsertAsync(user, lastLoginDateTime, performedBy);
        }   
        public async Task<bool> UpdateUserAsync(UserUpdateDTO user)
        {
            return await _userRepository.UpdateAsync(user);
        }
        public async Task<bool> DeleteUserAsync(string msid)
        {
            return await _userRepository.DeleteAsync(msid);
        }
        #endregion
    }
}