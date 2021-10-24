using CleanApi.Shared.DTOs.Catalog;
using FluentValidation;

namespace CleanApi.Application.Validators.Catalog
{
    public class CreateBrandRequestValidator : CustomValidator<CreateBrandRequest>
    {
        public CreateBrandRequestValidator()
        {
            RuleFor(p => p.Name).MaximumLength(75).NotEmpty();
        }
    }
}