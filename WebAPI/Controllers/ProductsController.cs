﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Products.AddProduct;
using Products.Application.Products.Dtos;
using Products.Application.Products.GetAllProducts;
using Products.Application.Products.ReduceStock;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-product")]
        public async Task<ActionResult<int>> AddProduct([FromBody] AddProductCommand addProduct)
        {
            int result = await _mediator.Send(addProduct).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("get-all-products")]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts(GetAllProductsQuery getAllProductsQuery)
        {
            var productsList = await _mediator.Send(getAllProductsQuery).ConfigureAwait(false);
            return Ok(productsList);
        }

        [HttpPost("reduce-stock")]
        public async Task<ActionResult<bool>> ReduceStock([FromBody] ReduceStockCommand reduceStockCommand)
        {
            bool reduceStock = await _mediator.Send(reduceStockCommand).ConfigureAwait(false);
            return Ok(reduceStock);
        }
    }
}
