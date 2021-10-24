using CleanApi.Application.Validators.General;
using CleanApi.Shared.DTOs.Catalog;
using FluentValidation;

namespace CleanApi.Application.Validators.Catalog
{
    public class UpdateProductRequestValidator : CustomValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(p => p.Name).MaximumLength(75).NotEmpty();
            RuleFor(p => p.Rate).GreaterThanOrEqualTo(1).NotEqual(0);
            RuleFor(p => p.Image).SetValidator(new FileUploadRequestValidator());
            RuleFor(p => p.BrandId).NotEmpty().NotNull();
        }
    }
}