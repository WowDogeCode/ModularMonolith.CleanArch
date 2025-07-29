using Common.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orders.Application.Abstraction.Repositories;
using Orders.Application.Orders;
using Orders.Application.Orders.AddOrder;
using Orders.Infrastructure.Repositories;
using Products.Application.Abstraction.Repositories;
using Products.Application.Products;
using Products.Application.Products.AddProduct;
using Products.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(ProductsMarker).Assembly, typeof(OrdersMarker).Assembly);
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IValidator<AddProductCommand>, AddProductValidator>();
builder.Services.AddScoped<IValidator<AddOrderCommand>, AddOrderValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
