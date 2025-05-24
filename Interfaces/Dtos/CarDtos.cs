using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Dtos
{
    public record CarDto(
        Guid Id, string Make, 
        string Model, int Year, 
        long Price, string Color, 
        DateTime DateAdded, DateTime DateSold, 
        bool IsSold, Guid UserId
        ) 
    { }

    public record CarDtoForCreate(
        string Make,
        string Model, int Year,
        long Price, string Color
        )
    { }

    public record CarDtoForUpdate(
        Guid Id,
        string Make, string Model,
        int Year, long Price,
        string Color
        )
    { }

    public record CarForSaleDto(
        Guid CarId, Guid UserId
        )
    { }
}
