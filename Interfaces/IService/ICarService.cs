using Interfaces.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.IService
{
    public interface ICarService
    {
        public Task CreateCarAsync(CarDtoForCreate carDto);
        public Task<List<CarDto>> GetAllCarsAsync();
        public Task<List<CarDto>> GetAllCarsByEmailAsync(string email);
        public Task<List<CarDto>> GetAllCarsOnSaleAsync();
        public Task UserBuysCarAsync(CarForSaleDto saleDto);
        public Task UpdateCarAsync(CarDtoForUpdate carDto);
        public Task DeleteCarAsync(Guid CarId);
    }
}
