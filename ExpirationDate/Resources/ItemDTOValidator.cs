using ExpirationDate.Models.DTO;
using FluentValidation;

namespace ExpirationDate.Resources
{
    public class ItemDTOValidator : AbstractValidator<ItemDTO>
    {
        public ItemDTOValidator(SharedLocalizationService localizer)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizer["Warning.RequiredFieldMessage"])
                .MinimumLength(3)
                .WithMessage(localizer["Warning.NameMinLengthMessage"])
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(localizer["Warning.RequiredFieldMessage"])
                .MinimumLength(7)
                .WithMessage(localizer["Warning.DescriptionMinLengthMessage"])
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage(localizer["Warning.RequiredFieldMessage"])
                .InclusiveBetween(0, 1000000)
                .Must(price => decimal.TryParse(price.ToString(), out decimal parsedPrice) && parsedPrice > 0)
                .WithMessage(localizer["Warning.PriceGreaterThanZero"]);
            
            RuleFor(x => x.Rating)
                .NotEmpty()
                .WithMessage(localizer["Warning.RequiredFieldMessage"])
                .InclusiveBetween(1, 5)
                .WithMessage(localizer["Warning.RatingBetweenOneAndFive"]);

            RuleFor(x => x.ExpirationDate)
                .NotEmpty()
                .WithMessage(localizer["Warning.RequiredFieldMessage"])
                .GreaterThan(DateTime.Now)
                .WithMessage(localizer["Warning.ExpirationDateInFuture"]);
        }
    }
}
/*
            .NotEmpty()
            .Must(price => price > 0)
            .GreaterThan(0)
            .WithMessage(localizer["Warning.PriceGreaterThanZero"]);
            */