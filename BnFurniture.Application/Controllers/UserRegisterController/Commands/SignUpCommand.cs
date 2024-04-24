using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ExampleController.DTO;
using BnFurniture.Application.Controllers.UserRegisterController.DTO;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BnFurniture.Domain.Entities;
using BnFurniture.Shared.Utilities.Hash;

namespace BnFurniture.Application.Controllers.UserRegisterController.Commands
{
    public static class SignUpCommand
    {
        // Тут описать входящие данные 
        // Напр. Модель формы, Id пользователя, ...
        public sealed record Command(UserSignUpDTO entityForm) : IRequest<Response>;



        // Описание модели / ответа, который отправится в контроллер после обработки
        public sealed class Response
        {

        }

        // Обработчик (Handler) запроса
        public sealed class Handler : CommandHandler<Command, Response>
        {
            private readonly IHashServices _hashServices;
            // Конструктор
            public Handler(IHandlerContext context, IHashServices hashServices)
                : base(context)
            {
                _hashServices = hashServices;
            }

            // Функция, в которой ведётся обработка запроса
            public override async ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                String ERRORMESSAGE= _ValidateModel(request.entityForm);

                return new Response();
            }
            //Проверяем базу данных по уже зарегистрированному такому логину
            bool IsLoginAlreadyUsed(string login)
            {
                return
                DbContext.User.Any(u => u.Email == login);
            }

            //Проверяем e-mail регулярным выражением
            bool IsValidEmail(string email)
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }

            //Проверка пароля на сложность
            bool IsPasswordStrong(string password)
            {
                // Проверка минимальной длины пароля
                if (password.Length < 8)
                {
                    return false;
                }

                // Проверка наличия букв верхнего регистра
                if (!password.Any(char.IsUpper))
                {
                    return false;
                }

                // Проверка наличия букв нижнего регистра
                if (!password.Any(char.IsLower))
                {
                    return false;
                }

                // Проверка наличия цифр
                if (!password.Any(char.IsDigit))
                {
                    return false;
                }

                // Проверка наличия специальных символов
                if (!password.Any(IsSpecialCharacter))
                {
                    return false;
                }

                return true;
            }

            bool IsSpecialCharacter(char c)
            {
                return !char.IsLetterOrDigit(c);
            }

            //Проверка валидности данных в модели, принятой из формы
            public string _ValidateModel(UserSignUpDTO? model)
            {
                if (model == null) { return "Данные не переданы"; }
                if (string.IsNullOrEmpty(model.Login)) { return "Логин не может быть пустым"; }
                if (IsLoginAlreadyUsed(model.Login)) { return "Введений вами логін вже використовується"; }
                if (!IsValidEmail(model.Login)) { return "Email is not valid"; }
                if (string.IsNullOrEmpty(model.Password)) { return "Пароль не может быть пустым"; }
                if (model.Password != model.RepeatPassword) { return "Пароли должны быть одинаковыми"; }
                // if (!IsPasswordStrong(model.Password)) { return "Пароль должен содержать цифры буквы верхнего и нижнего регистра и специальные символы"; }
                if (string.IsNullOrEmpty(model.Name)) { return "Имя не может быть пустым"; }
                if (string.IsNullOrEmpty(model.LastName)) { return "Фамилия не может быть пустым"; }
                if (!model.Agree) { return "Надо соглашаться"; }

                // добавляем пользователя в БД
                DbContext.User.Add(new Domain.Entities.User
                {
                    Id = Guid.NewGuid(),
                    Email = model.Login,
                    Password = _hashServices.HashString(model.Password),
                    FirstName = model.Name,
                    LastName = model.LastName,
                    Address = model.Address,
                    Phonenumber = model.Phone,
                    Created = DateTime.Now,
                    LastLogin_At = DateTime.Now,
                });

                // сохраняем внесенные изменения
                DbContext.SaveChanges();

                return String.Empty;

            }
        }
    }
}
