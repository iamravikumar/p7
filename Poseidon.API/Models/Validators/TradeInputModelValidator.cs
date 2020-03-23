using System;
using FluentValidation;

namespace Poseidon.API.Models.Validators
{
    public class TradeInputModelValidator : AbstractValidator<TradeInputModel>
    {
        public TradeInputModelValidator()
        {
            RuleFor(model => model.Account)
                .MaximumLength(100);
            
            RuleFor(model => model.Type)
                .MaximumLength(100);

            RuleFor(model => model.BuyQuantity)
                .InclusiveBetween(1, double.MaxValue);
            
            RuleFor(model => model.SellQuantity)
                .InclusiveBetween(1, double.MaxValue);
            
            RuleFor(model => model.BuyPrice)
                .InclusiveBetween((decimal) 1.0, decimal.MaxValue);
            
            RuleFor(model => model.SellPrice)
                .InclusiveBetween((decimal) 1.0, decimal.MaxValue);

            RuleFor(model => model.TradeDate)
                .GreaterThan(new DateTime(2019, 01, 01));
            
            RuleFor(model => model.Security)
                .MaximumLength(100);
            
            RuleFor(model => model.Status)
                .MaximumLength(100);
            
            RuleFor(model => model.Trader)
                .MaximumLength(100);
            
            RuleFor(model => model.Benchmark)
                .MaximumLength(100);
            
            RuleFor(model => model.Book)
                .MaximumLength(100);
            
            RuleFor(model => model.CreationName)
                .MaximumLength(100);
            
            RuleFor(model => model.CreationDate)
                .GreaterThan(new DateTime(2019, 01, 01));
            
            RuleFor(model => model.RevisionName)
                .MaximumLength(100);
            
            RuleFor(model => model.RevisionDate)
                .GreaterThan(new DateTime(2019, 01, 01));
            
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