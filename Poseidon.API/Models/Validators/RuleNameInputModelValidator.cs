using FluentValidation;

namespace Poseidon.API.Models.Validators
{
    public class RuleNameInputModelValidator : AbstractValidator<RuleNameInputModel>
    {
        public RuleNameInputModelValidator()
        {
            RuleFor(model => model.Name)
                .MaximumLength(100);
            
            RuleFor(model => model.Description)
                .MaximumLength(100);
            
            RuleFor(model => model.Json)
                .MaximumLength(100);
            
            RuleFor(model => model.Template)
                .MaximumLength(100);
            
            RuleFor(model => model.SqlStr)
                .MaximumLength(100);
            
            RuleFor(model => model.SqlPart)
                .MaximumLength(100);
        }
    }
}