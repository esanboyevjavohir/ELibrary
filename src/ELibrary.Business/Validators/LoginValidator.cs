using ELibrary.Business.Models.User;
using FluentValidation;

namespace ELibrary.Business.Validators
{
    public class LoginValidator : AbstractValidator<LoginUserModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}
