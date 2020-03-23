using FluentValidation;

namespace Poseidon.API.Models.Validators
{
    public class BidListInputModelValidator : AbstractValidator<BidListInputModel>
    {
        public BidListInputModelValidator()
        {
            RuleFor(model => model.Account)
                .MaximumLength(100);

            RuleFor(model => model.Type)
                .MaximumLength(100);

            RuleFor(model => model.BidQuantity)
                .InclusiveBetween(0, double.MaxValue);

            RuleFor(model => model.AskQuantity)
                .InclusiveBetween(0, double.MaxValue);

            RuleFor(model => model.Bid)
                .InclusiveBetween(0, double.MaxValue);

            RuleFor(model => model.Ask)
                .InclusiveBetween(0, double.MaxValue);

            RuleFor(model => model.Benchmark)
                .MaximumLength(100);

            RuleFor(model => model.Commentary)
                .MaximumLength(100);
            
            RuleFor(model => model.Security)
                .MaximumLength(100);
            
            RuleFor(model => model.Status)
                .MaximumLength(100);
            
            RuleFor(model => model.Trader)
                .MaximumLength(100);
            
            RuleFor(model => model.Book)
                .MaximumLength(100);
            
            RuleFor(model => model.CreationName)
                .MaximumLength(100);
            
            RuleFor(model => model.RevisionName)
                .MaximumLength(100);
            
            RuleFor(model => model.DealName)
                .MaximumLength(100);
            
            RuleFor(model => model.DealType)
                .MaximumLength(100);
            
            RuleFor(model => model.SourceListId)
                .MaximumLength(100);
            
            RuleFor(model => model.Side)
                .MaximumLength(100);
        }
    }
}