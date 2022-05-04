using Application.Core;
using Application.Core.Models;
using Application.Core.Services.Concrete;
using FluentValidation;

namespace Application.Api
{
    public class UserRegisterValidator : AbstractValidator<UserModelDTO>
    {
        public UserRegisterValidator()
        {
            RuleFor(user => user.password).NotEmpty()
                .MaximumLength(12)
                .MinimumLength(6)
                .NotEqual(user => user.Name)
                    .WithMessage("The password shouldn't be equal with the name")
                .NotEqual(user => user.LastName)
                    .WithMessage("The password shouldn't be equal with the lastname");

            RuleFor(user => user.email).NotEmpty()
                .NotNull()
                    .WithMessage("Email is required")
                .EmailAddress()
                    .WithMessage("A valid email is required");
              
            RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required")
                .MaximumLength(10);

            RuleFor(user => user.LastName).NotEmpty().WithMessage("Lastname is required");
        }
    }
}
