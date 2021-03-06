using CleanApi.Application.Abstractions.Services.Catalog;
using CleanApi.Domain.Constants;
using CleanApi.Infrastructure.Identity.Permissions;
using CleanApi.Shared.DTOs.Catalog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanApi.Bootstrapper.Controllers.v1
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [MustHavePermission(Permissions.Products.View)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var product = await _service.GetProductDetailsAsync(id);
            return Ok(product);
        }

        [HttpPost("search")]
        [MustHavePermission(Permissions.Products.Search)]
        public async Task<IActionResult> SearchAsync(ProductListFilter filter)
        {
            var products = await _service.SearchAsync(filter);
            return Ok(products);
        }

        [HttpGet("dapper")]
        [MustHavePermission(Permissions.Products.View)]
        public async Task<IActionResult> GetDapperAsync(Guid id)
        {
            var products = await _service.GetByIdUsingDapperAsync(id);
            return Ok(products);
        }

        [HttpPost]
        [MustHavePermission(Permissions.Products.Register)]
        public async Task<IActionResult> CreateAsync(CreateProductRequest request)
        {
            return Ok(await _service.CreateProductAsync(request));
        }

        [HttpPut]
        [MustHavePermission(Permissions.Products.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateProductRequest request, Guid id)
        {
            return Ok(await _service.UpdateProductAsync(request, id));
        }

        [HttpDelete]
        [MustHavePermission(Permissions.Products.Remove)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var productId = await _service.DeleteProductAsync(id);
            return Ok(productId);
        }
    }
}