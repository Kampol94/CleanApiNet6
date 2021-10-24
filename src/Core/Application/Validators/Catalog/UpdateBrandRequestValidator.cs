using CleanApi.Shared.DTOs.Catalog;
using FluentValidation;

namespace CleanApi.Application.Validators.Catalog
{
    public class UpdateBrandRequestValidator : CustomValidator<UpdateBrandRequest>
    {
        public UpdateBrandRequestValidator()
        {
            RuleFor(p => p.Name).MaximumLength(75).NotEmpty();
        }
    }
}