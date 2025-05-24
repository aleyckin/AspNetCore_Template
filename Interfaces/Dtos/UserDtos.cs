using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Dtos
{
    public record UserDto(Guid Id, string Email, DateTime DateCreated, DateTime LastLogin) { }
    public record UserDtoForUpdate(string Email, string Password) { }
    public record UserDtoCredentials(string Email, string Password) { }
}
