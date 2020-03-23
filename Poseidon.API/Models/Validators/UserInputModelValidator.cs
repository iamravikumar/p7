using FluentValidation;

namespace Poseidon.API.Models.Validators
{
    public class UserInputModelValidator : AbstractValidator<UserInputModel>
    {
        public UserInputModelValidator()
        {
            RuleFor(model => model.Username)
                .MaximumLength(100);

            RuleFor(model => model.Password)
                .MaximumLength(100);

            RuleFor(model => model.FullName)
                .MaximumLength(100);

            RuleFor(model => model.Role)
                .MaximumLength(100);
        }
    }
}