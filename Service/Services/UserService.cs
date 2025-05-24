using AutoMapper;
using Domain.Entities;
using Interfaces.Dtos;
using Interfaces.IService;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Service.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Exceptions.AppException;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;

        public UserService(DataContext dataContext, IMapper mapper, IJwtGenerator jwtGenerator)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
        }

        public async Task DeleteUserByEmailAsync(string email)
        {
            var user = await GetUserEntityByEmailAsync(email);

            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _mapper
                .ProjectTo<UserDto>(_dataContext.Users)
                .ToListAsync();
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await GetUserEntityByEmailAsync(email);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<string> RegisterUserAsync(UserDtoCredentials userDto)
        {
            bool userExist = await _dataContext.Users
                .AnyAsync(x => x.Email == userDto.Email);

            if (userExist)
            {
                throw new UserAlreadyExistException(userDto.Email);
            }

            var user = _mapper.Map<User>(userDto);

            byte[] salt;
            user.PasswordHash = PasswordHasher.HashPassword(userDto.Password, out salt);
            user.PasswordSalt = salt;
            user.DateCreated = DateTime.UtcNow;
            user.LastLogin = DateTime.UtcNow;

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return _jwtGenerator.GenerateJwtToken(user);
        }

        public async Task UpdateUserByEmailAsync(UserDtoForUpdate userDto)
        {
            var user = await GetUserEntityByEmailAsync(userDto.Email);

            _mapper.Map(userDto, user);

            byte[] salt;
            user.PasswordHash = PasswordHasher.HashPassword(userDto.Password, out salt);
            user.PasswordSalt = salt;

            await _dataContext.SaveChangesAsync();
        }

        public async Task<string> ValidateUserCredentialsAsync(UserDtoCredentials userDto)
        {
            var user = await GetUserEntityByEmailAsync(userDto.Email);

            bool IsValidCredentials = PasswordHasher.VerifyPassword(userDto.Password, user.PasswordHash, user.PasswordSalt);
            if (IsValidCredentials)
            {
                user.LastLogin = DateTime.UtcNow;
                await _dataContext.SaveChangesAsync();
            }
            if (!IsValidCredentials)
            {
                throw new InvalidCredentialsException();
            }
            return _jwtGenerator.GenerateJwtToken(user);
        } 
        
        private async Task<User> GetUserEntityByEmailAsync(string email)
        {
            var user = await _dataContext.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                throw new UserNotFoundException(email);
            }

            return user;
        }
    }
}
