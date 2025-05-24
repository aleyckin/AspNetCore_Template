using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; }

        public AppException(string message, int statusCode = 400) : base(message) { StatusCode = statusCode; }

        public class UserNotFoundException : AppException
        {
            public UserNotFoundException(string email) : base($"Пользователь c такой почтой {email} не найден.", 404) { }
        }

        public class UserAlreadyExistException : AppException
        {
            public UserAlreadyExistException(string email) : base($"Пользователь c такой почтой {email} уже существует.", 400) { }
        }

        public class InvalidCredentialsException : AppException
        {
            public InvalidCredentialsException() : base($"Неверные данные.", 400) { }
        }

        public class CarNotFoundException : AppException
        {
            public CarNotFoundException(Guid carId) : base($"Автомобиль {carId} не найден.", 404) { }
        }
    }
}
