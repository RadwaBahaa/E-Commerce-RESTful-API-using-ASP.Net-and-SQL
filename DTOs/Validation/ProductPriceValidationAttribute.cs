using System.ComponentModel.DataAnnotations;

namespace DTOs.Validation
{
    public class ProductPriceValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var price = (float)value;
            if (price == 0)
            {
                return new ValidationResult("You must include the 'product's price' !....");
            }
            else
            {
                if (price > 0)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The 'product price' must be higher than zero EGP !....");
                }
            }
        }
    }
}
