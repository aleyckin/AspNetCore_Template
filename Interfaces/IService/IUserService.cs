using Interfaces.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.IService
{
    public interface IUserService
    {
        public Task<string> RegisterUserAsync(UserDtoCredentials userDto);
        public Task<string> ValidateUserCredentialsAsync(UserDtoCredentials userDto);
        public Task<List<UserDto>> GetAllUsersAsync();
        public Task<UserDto> GetUserByEmailAsync(string email);
        public Task UpdateUserByEmailAsync(UserDtoForUpdate userDto);
        public Task DeleteUserByEmailAsync(string email);
    }
}
