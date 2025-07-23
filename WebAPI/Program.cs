using Common.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
    cfg.RegisterServicesFromAssembly(typeof(ProductsMarker).Assembly);
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IValidator<AddProductCommand>, AddProductValidator>();

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
