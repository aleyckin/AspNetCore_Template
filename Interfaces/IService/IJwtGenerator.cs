using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.IService
{
    public interface IJwtGenerator
    {
        string GenerateJwtToken(User user);
    }
}
