using Interfaces.Dtos;
using Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/car")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>
        /// Получение списка всех автомобилей.
        /// </summary>
        /// <returns>Список автомобилей в формате CarDto.</returns>
        [HttpGet("getAllCars")]
        public async Task<ActionResult<List<CarDto>>> GetAllCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        /// <summary>
        /// Получение списка всех автомобилей, выставленных на продажу.
        /// </summary>
        /// <returns>Список автомобилей в формате CarDto.</returns>
        [HttpGet("getAllCarsForSale")]
        public async Task<ActionResult<List<CarDto>>> GetAllCarsForSale()
        {
            var cars = await _carService.GetAllCarsOnSaleAsync();
            return Ok(cars);
        }

        /// <summary>
        /// Получение списка всех автомобилей, принадлежащих пользователю.
        /// </summary>
        /// <param name="email">Email пользователя, чьи автомобили необходимо получить.</param>
        /// <returns>Список автомобилей в формате CarDto.</returns>
        [HttpGet("getAllCarsForUser/{email}")]
        public async Task<ActionResult<List<CarDto>>> GetAllCarsForUser(string email)
        {
            var cars = await _carService.GetAllCarsByEmailAsync(email);
            return Ok(cars);
        }

        /// <summary>
        /// Создание нового автомобиля.
        /// </summary>
        /// <param name="carDto">Данные для создания автомобиля.</param>
        /// <returns>Результат выполнения операции.</returns>
        [HttpPost("createCar")]
        public async Task<IActionResult> CreateCar([FromBody] CarDtoForCreate carDto)
        {
            await _carService.CreateCarAsync(carDto);
            return Ok();
        }

        /// <summary>
        /// Изменение информации автомобиля.
        /// </summary>
        /// <param name="carDto">Обновленные данные автомобиля.</param>
        /// <returns>Результат выполнения операции.</returns>
        [HttpPut("updateCar")]
        public async Task<IActionResult> UpdateCar([FromBody] CarDtoForUpdate carDto)
        {
            await _carService.UpdateCarAsync(carDto);
            return Ok();
        }

        /// <summary>
        /// Удаление автомобиля по идентификатору.
        /// </summary>
        /// <param name="carId">Идентификатор автомобиля.</param>
        /// <returns>Результат выполнения операции.</returns>
        [HttpDelete("deleteCar/{carId:guid}")]
        public async Task<IActionResult> DeleteCar(Guid carId)
        {
            await _carService.DeleteCarAsync(carId);
            return Ok();
        }

        /// <summary>
        /// Покупка автомобиля пользователем.
        /// </summary>
        /// <param name="saleDto">Данные о покупке автомобиля.</param>
        /// <returns>Результат выполнения операции.</returns>
        [HttpPost("BuyCar")]
        public async Task<IActionResult> BuyCar([FromBody] CarForSaleDto saleDto)
        {
            await _carService.UserBuysCarAsync(saleDto);
            return Ok();
        }
    }
}
