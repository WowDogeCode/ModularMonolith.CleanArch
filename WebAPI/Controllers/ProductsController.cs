using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Products;

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
    }
}
