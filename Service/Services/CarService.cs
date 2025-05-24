using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Interfaces.Dtos;
using Interfaces.IService;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Exceptions.AppException;

namespace Service.Services
{
    public class CarService : ICarService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CarService(DataContext dataContext,  IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task CreateCarAsync(CarDtoForCreate carDto)
        {
            _dataContext.Cars.Add(_mapper.Map<CarDtoForCreate, Car>(carDto));
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(Guid carId)
        {
            var car = await GetCarEntityAsync(carId);

            _dataContext.Remove(car);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<CarDto>> GetAllCarsAsync()
        {
            return await _mapper
                .ProjectTo<CarDto>(_dataContext.Cars)
                .ToListAsync();
        }

        public async Task<List<CarDto>> GetAllCarsByEmailAsync(string email)
        {
            return await _mapper
                .ProjectTo<CarDto>(_dataContext.Cars
                .Where(x => x.Owner.Email == email))
                .ToListAsync();
        }

        public async Task<List<CarDto>> GetAllCarsOnSaleAsync()
        {
            return await _mapper
                .ProjectTo<CarDto>(_dataContext.Cars
                .Where(x => !x.IsSold))
                .ToListAsync();
        }

        public async Task UpdateCarAsync(CarDtoForUpdate carDto)
        {
            var car = await GetCarEntityAsync(carDto.Id);

            _mapper.Map(carDto, car);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UserBuysCarAsync(CarForSaleDto saleDto)
        {
            var car = await GetCarEntityAsync(saleDto.CarId);

            car.IsSold = true;
            car.DateSold = DateTime.UtcNow;
            car.UserId = saleDto.UserId;
            await _dataContext.SaveChangesAsync();
        }

        private async Task<Car> GetCarEntityAsync(Guid carId)
        {
            var car = await _dataContext.Cars
                .FirstOrDefaultAsync(x => x.Id == carId);

            if (car == null)
            {
                throw new CarNotFoundException(carId);
            }

            return car;
        }
    }
}
