    using ASP_Work.Services.MailSend;
    using BnFurniture.Infrastructure.Persistence;
    using BnFurniture.Shared.Utilities.Hash;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace BnFurniture.Application.Controllers.UserRegisterController.DTO;

        public class UserPassForgotDTO
        {
            [FromForm(Name = "emailOrPhone")]
            public string Email { get; set; } = null!;
        }
    //public class PassForgotDTOValidator : AbstractValidator<UserPassForgotDTO>
    //{
    //    private readonly ApplicationDbContext _dbContext;
    //    private readonly IHashService _hashService;
    //    private readonly IMailServices _mailServices;

    //    public PassForgotDTOValidator(ApplicationDbContext dbContext, IHashService hashService, IMailServices mailServices)
    //    {
    //        _dbContext = dbContext;
    //        _hashService = hashService;
    //        _mailServices = mailServices;


    //        RuleFor(x => x.Email)
    //            .NotEmpty().WithMessage("Логін не може бути порожнім.");
    //    }

    //    public Task<IActionResult> ForgotPassword(string login)
    //    {
    //        var user = _dbContext.User.FirstOrDefault(u => u.Email == login);
    //        if (user != null)
    //        {
    //            var newPassword = GeneratePassword();
    //            user.Password = _hashService.HashString(newPassword);
    //            _dbContext.User.Update(user);
    //            _dbContext.SaveChanges();
    //            _mailServices.SendMess(newPassword, login);
    //            var result = new { status = "OK", newPassword = newPassword, mail = "Send message" };
    //            return Task.FromResult((IActionResult)Ok(result));


    //        }
    //        else
    //        {
    //            var result=(new { status = "NO", message = "Пользователь с указанным логином не найден." });
    //            return Task.FromResult((IActionResult)No(result));

    //        }
    //    }

    //    private string GeneratePassword(int length = 12)
    //    {
    //        const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+{}[]|\\:;\"',./<>?`~";
    //        StringBuilder sb = new StringBuilder();
    //        Random rnd = new Random();

    //        for (int i = 0; i < length; i++)
    //        {
    //            int index = rnd.Next(validChars.Length);
    //            sb.Append(validChars[index]);
    //        }

    //        return sb.ToString();
    //    }
    //}
