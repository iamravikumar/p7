using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;
using Poseidon.API.Models;

namespace Poseidon.API.Validators
{
    public class BidListInputModelValidator : AbstractValidator<BidListInputModel>
    {
        public BidListInputModelValidator()
        {
            RuleFor(dto => dto.Account)
                .MaximumLength(100);

            RuleFor(dto => dto.Type)
                .MaximumLength(100);

            RuleFor(dto => dto.BidQuantity)
                .InclusiveBetween(0, double.MaxValue);

            RuleFor(dto => dto.AskQuantity)
                .InclusiveBetween(0, double.MaxValue);

            RuleFor(dto => dto.Bid)
                .InclusiveBetween(0, double.MaxValue);

            RuleFor(dto => dto.Ask)
                .InclusiveBetween(0, double.MaxValue);

            RuleFor(dto => dto.Benchmark)
                .MaximumLength(100);

            RuleFor(dto => dto.Commentary)
                .MaximumLength(100);
            
            RuleFor(dto => dto.Security)
                .MaximumLength(100);
            
            RuleFor(dto => dto.Status)
                .MaximumLength(100);
            
            RuleFor(dto => dto.Trader)
                .MaximumLength(100);
            
            RuleFor(dto => dto.Book)
                .MaximumLength(100);
            
            RuleFor(dto => dto.CreationName)
                .MaximumLength(100);
            
            RuleFor(dto => dto.RevisionName)
                .MaximumLength(100);
            
            RuleFor(dto => dto.DealName)
                .MaximumLength(100);
            
            RuleFor(dto => dto.DealType)
                .MaximumLength(100);
            
            RuleFor(dto => dto.SourceListId)
                .MaximumLength(100);
            
            RuleFor(dto => dto.Side)
                .MaximumLength(100);
        }
    }
}