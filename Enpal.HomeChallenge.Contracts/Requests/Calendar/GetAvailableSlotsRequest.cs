using System.ComponentModel.DataAnnotations;

namespace Enpal.HomeChallenge.Contracts.Requests.Calendar;

public sealed record GetAvailableSlotsRequest : IValidatableObject
{
    [Required] public DateOnly Date { get; set; }
    [Required] public IEnumerable<string> Products { get; set; }
    [Required] public string Language { get; set; }
    [Required] public string Rating { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var allowedProducts = Constants.AvailableProducts;
        if (Products.Any(x => !allowedProducts.Contains(x)))
        {
            yield return new ValidationResult(
                $"Invalid product. Should be one of: {string.Join(", ", allowedProducts)}",
                new[] { nameof(Products) });
        }

        if (!Constants.SalesManagers.AvailableLanguages.Contains(Language))
        {
            yield return new ValidationResult(
                $"Invalid language. Should be one of: {string.Join(", ", Constants.SalesManagers.AvailableLanguages)}",
                new[] { nameof(Language) });
        }

        if (!Constants.SalesManagers.AvailableRatings.Contains(Rating))
        {
            yield return new ValidationResult(
                $"Invalid rating. Should be one of: {string.Join(", ", Constants.SalesManagers.AvailableRatings)}",
                new[] { nameof(Rating) });
        }
    }
}