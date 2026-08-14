using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Products.AddProduct;
using Products.Application.Products.DTOs;
using Products.Application.Products.DTOs.Requests;
using Products.Application.Products.GetAllProducts;
using Products.Application.Products.ReduceStock;
using Products.Application.Products.UpdateProductPrice;
using Products.Application.Products.UpdateProductStock;

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
        public async Task<ActionResult<int>> AddProduct([FromBody] AddProductRequestDto addProduct)
        {
            var command = new AddProductCommand
            {
                CategoryId = addProduct.CategoryId,
                Discontinued = addProduct.Discontinued,
                ProductName = addProduct.ProductName,
                QuantityPerUnit = addProduct.QuantityPerUnit,
                ReorderLevel = addProduct.ReorderLevel,
                SupplierId = addProduct.SupplierId,
                UnitPrice = addProduct.UnitPrice,
                UnitsInStock = addProduct.UnitsInStock,
                UnitsOnOrder = addProduct.UnitsOnOrder
            };

            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result.ProductId);
        }

        [HttpGet("get-all-products")]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            var query = new GetAllProductsQuery { };

            var productsList = await _mediator.Send(query).ConfigureAwait(false);
            return Ok(productsList);
        }

        [HttpPost("reduce-stock")]
        public async Task<ActionResult<bool>> ReduceStock([FromBody] ReduceStockRequestDto reduceStock)
        {
            var command = new ReduceStockCommand
            {
                ProductId = reduceStock.ProductId,
                Quantity = reduceStock.Quantity
            };

            bool reduceStockResult = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(reduceStockResult);
        }

        [HttpPost("update-product-price")]
        public async Task<ActionResult<ProductDto>> UpdateProductPrice([FromBody] UpdateProductPriceRequestDto updateProductPrice)
        {
            var command = new UpdateProductPriceCommand
            {
                Price = updateProductPrice.Price,
                ProductId = updateProductPrice.ProductId
            };

            var product = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(product);
        }

        [HttpPost("update-product-stock")]
        public async Task<ActionResult<ProductDto>> UpdateProductStock([FromBody] UpdateProductStockRequestDto updateProductStock)
        {
            var command = new UpdateProductStockCommand
            {
                ProductId = updateProductStock.ProductId,
                Stock = updateProductStock.Stock
            };

            var product = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(product);
        }
    }
}