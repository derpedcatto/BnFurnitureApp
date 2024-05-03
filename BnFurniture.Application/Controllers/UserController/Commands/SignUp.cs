using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserController.DTO;
using Mediator;
using BnFurniture.Shared.Utilities.Hash;
using FluentValidation;

namespace BnFurniture.Application.Controllers.UserController.Commands;

public static class SignUp
{
    public sealed record Command(UserSignUpDTO entityForm) : IRequest<Response>;

    public sealed class Response
    {

    }

    public sealed class Handler : CommandHandler<Command, Response>
    {
        private readonly IHashService _hashService;


        public Handler(IHandlerContext context, IHashService hashService)
            : base(context)
        {
            _hashService = hashService;
        }


        public override async ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.entityForm;
            /*
            var validator = new UserSignUpDTOValidator(DbContext);
            validator.ValidateAndThrow(model);
            */

            // await SaveUser(model);
            return new Response();
        }

        private async Task SaveUser(UserSignUpDTO dto)
        {
            await DbContext.AddAsync(new Domain.Entities.User
            {
                Id = Guid.NewGuid(),
                Email = dto.Login,
                Phonenumber = dto.Phone,
                Password = _hashService.HashString(dto.Password),
                FirstName = dto.Name, 
                LastName = dto.LastName,
                Address = dto.Address,
                Created = DateTime.Now
            });
            await DbContext.SaveChangesAsync();
        }
    }
}

/*
var validationResult = validator.Validate(model);
if (!validationResult.IsValid)
{
    foreach (var error in validationResult.Errors)
    {
        Logger.LogError($"[ERROR] {error.ErrorCode} - {error.ErrorMessage}");
    }
}
*/