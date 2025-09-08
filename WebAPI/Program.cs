using Common.Application.Abstraction;
using Common.Infrastructure;
using Common.Infrastructure.UoW;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Orders.Application.Abstraction.Repositories;
using Orders.Application.Orders;
using Orders.Application.Orders.PlaceOrder;
using Orders.Infrastructure.Repositories;
using Products.Application.Abstraction.Repositories;
using Products.Application.Products;
using Products.Application.Products.AddProduct;
using Products.Application.Products.ReduceStock;
using Products.Application.Services;
using Products.Infrastructure.Repositories;
using System.Data;

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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddTransient<IDbConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    return new SqlConnection(connectionString);
});

builder.Services.AddScoped<IValidator<AddProductCommand>, AddProductValidator>();
builder.Services.AddScoped<IValidator<PlaceOrderCommand>, PlaceOrderValidator>();
builder.Services.AddScoped<IValidator<ReduceStockCommand>, ReduceStockValidator>();

builder.Services.AddScoped<IInventoryService, InventoryService>();

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
