using System;
using FluentValidation;

namespace Poseidon.API.Models.Validators
{
    public class CurvePointInputModelValidator : AbstractValidator<CurvePointInputModel>
    {
        public CurvePointInputModelValidator()
        {
            RuleFor(model => model.CurveId)
                .InclusiveBetween((short) 1, short.MaxValue);
            
            RuleFor(model => model.AsOfDate)
                .GreaterThan(new DateTime(2019, 01, 01));

            RuleFor(model => model.Term)
                .InclusiveBetween((double) 0.0, double.MaxValue);

            RuleFor(model => model.Value)
                .InclusiveBetween((double) 0.0, double.MaxValue);
            
            RuleFor(model => model.CreationDate)
                .GreaterThan(new DateTime(2019, 01, 01));
        }
    }
}