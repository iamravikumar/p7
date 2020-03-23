using FluentValidation;

namespace Poseidon.API.Models.Validators
{
    public class RatingInputModelValidator : AbstractValidator<RatingInputModel>
    {
        public RatingInputModelValidator()
        {
            RuleFor(model => model.MoodysRating)
                .MaximumLength(10);
            
            RuleFor(model => model.SandPrating)
                .MaximumLength(10);
            
            RuleFor(model => model.FitchRating)
                .MaximumLength(10);
            
            RuleFor(model => model.OrderNumber)
                .InclusiveBetween((short) 1, short.MaxValue);
        }
    }
}