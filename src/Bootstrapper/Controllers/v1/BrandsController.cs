using CleanApi.Application.Abstractions.Services.Catalog;
using CleanApi.Domain.Constants;
using CleanApi.Infrastructure.Identity.Permissions;
using CleanApi.Shared.DTOs.Catalog;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace CleanApi.Bootstrapper.Controllers.v1
{
    public class BrandsController : BaseController
    {
        private readonly IBrandService _service;

        public BrandsController(IBrandService service)
        {
            _service = service;
        }

        [HttpPost("search")]
        [MustHavePermission(Permissions.Brands.Search)]
        [SwaggerOperation(Summary = "Search Brands using available Filters.")]
        public async Task<IActionResult> SearchAsync(BrandListFilter filter)
        {
            var brands = await _service.SearchAsync(filter);
            return Ok(brands);
        }

        [HttpPost]
        [MustHavePermission(Permissions.Brands.Register)]
        public async Task<IActionResult> CreateAsync(CreateBrandRequest request)
        {
            return Ok(await _service.CreateBrandAsync(request));
        }

        [HttpPut]
        [MustHavePermission(Permissions.Brands.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateBrandRequest request, Guid id)
        {
            return Ok(await _service.UpdateBrandAsync(request, id));
        }

        [HttpDelete]
        [MustHavePermission(Permissions.Brands.Remove)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var brandId = await _service.DeleteBrandAsync(id);
            return Ok(brandId);
        }
    }
}