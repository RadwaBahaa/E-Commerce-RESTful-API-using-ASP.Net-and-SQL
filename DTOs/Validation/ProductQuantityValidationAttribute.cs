using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Validation
{
    internal class ProductQuantityValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var productQuatity = Convert.ToInt32(value);
            if (productQuatity == 0)
            {
                return new ValidationResult("You must include the product's quatity !....");
            }
            else
            {
                if (productQuatity > 0)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The 'product quatity' must be positive !....");
                }
            }
        }
    }
}
