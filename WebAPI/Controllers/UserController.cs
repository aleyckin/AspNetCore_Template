using Interfaces.Dtos;
using Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Регистрация нового пользователя и получение JWT.
        /// </summary>
        /// /// <param name="userDto">Данные учетной записи пользователя (логин и пароль).</param>
        /// <returns>JWT</returns>
        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterUser([FromBody] UserDtoCredentials userDto)
        {
            var token = await _userService.RegisterUserAsync(userDto);
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Аутентификация пользователя и получение JWT токена.
        /// </summary>
        /// <param name="userDto">Данные учетной записи пользователя (почта и пароль).</param>
        /// <returns>JWT токен для аутентифицированного пользователя.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserDtoCredentials userDto)
        {
            var token = await _userService.ValidateUserCredentialsAsync(userDto);
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Получение списка всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        [Authorize]
        [HttpGet("getAllUsers")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            return await _userService.GetAllUsersAsync();
        }

        /// <summary>
        /// Получение информации о пользователе по указанному Email.
        /// </summary>
        /// <param name="email">Email пользователя, информацию о котором необходимо получить.</param>
        /// <returns>Данные пользователя в формате UserDto.</returns>
        [Authorize]
        [HttpGet("getUserByEmail/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            return await _userService.GetUserByEmailAsync(email);
        }

        /// <summary>
        /// Изменение информации о пользователе по Email.
        /// </summary>
        /// <param name="userDto">Обновленные данные пользователя, включая Email.</param>
        /// <returns>Результат выполнения операции.</returns>
        [Authorize]
        [HttpPut("updateUserByEmail")]
        public async Task<ActionResult<UserDto>> UpdateUserByEmail([FromBody] UserDtoForUpdate userDto)
        {
            await _userService.UpdateUserByEmailAsync(userDto);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя по Email.
        /// </summary>
        /// <param name="email">Email пользователя, которого необходимо удалить.</param>
        /// <returns>Результат выполнения операции.</returns>
        [Authorize]
        [HttpDelete("deleteUserByEmail/{email}")]
        public async Task<ActionResult<UserDto>> DeleteUserByEmail(string email)
        {
            await _userService.DeleteUserByEmailAsync(email);
            return Ok();
        }
    }
}
