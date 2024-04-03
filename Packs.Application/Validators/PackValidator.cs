using FluentValidation;
using Packs.Application.Models;

namespace Packs.Application.Validators;
public class PackValidator : AbstractValidator<Pack>
{
    public PackValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        RuleFor(x => x.DisplayName)
            .NotEmpty();
        RuleFor(x => x.Category)
            .NotEmpty();
        RuleFor(x => x.Categories_listed_in)
            .NotEmpty();
        RuleFor(x => x.NumberOfImages)
            .NotEmpty()
            .GreaterThan(0).WithMessage("The NumberOfImages number must be greater than 0.");
        RuleFor(x => x.DefaultImage)
            .NotEmpty()
            .GreaterThan(0).WithMessage("The DefaultImage number must be greater than 0.");
        RuleFor(x => x.SortOrderWithinCategory)
            .NotEmpty()
            .GreaterThan(0).WithMessage("The SortOrderWithinCategory number must be greater than 0.");
        RuleFor(x => x.StoreProductId)
            .NotEmpty();
        RuleFor(x => x.PackVersion)
            .NotEmpty()
            .GreaterThan(0).WithMessage("The PackVersion number must be greater than 0.");
    }
}
