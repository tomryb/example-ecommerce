using Backend;
using Backend.Adapters;
using Backend.Implementations;
using Backend.InventoryModule;
using Backend.MockImplementations;
using Backend.ProductCatalogModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BackendDbContext>(opt => opt.UseInMemoryDatabase("mock"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
var app = builder.Build();

// When implementing Auth, uncomment this line:
// app.MapGet("/", [Authorize] () => "Hello World!");
// and comment this one
app.MapGet("/", () => "Hello World!");

MockAuthorizationService mockAuthorizationService = new();

new ProductCatalogModule()
    .AddModule(new AuthorizationAdapters(mockAuthorizationService.Authorize))
    .ToList()
    .ForEach(endpoint => app.MapMethods(endpoint.Path, new[] { endpoint.Method.Method }, endpoint.Handler));

new InventoryModule()
    .AddModule(new AuthorizationAdapters(mockAuthorizationService.Authorize))
    .ToList()
    .ForEach(endpoint => app.MapMethods(endpoint.Path, new[] { endpoint.Method.Method }, endpoint.Handler));

app.Run();
