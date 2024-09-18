var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarterModulesFromAssemblies
(
    typeof(CreateGameEndpoint).Assembly
// ShoppingCart module assembly gelecek
// Ordering module assembly gelecek
);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddShoppingCartModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandler<CentralExceptionHandler>();

var app = builder.Build();

// Carter paketini kullanarak route map işlemleri icra edilecek
app.MapCarter();

app
    .UseCatalogModule()
    .UseShoppingCartModule()
    .UseOrderingModule();

app.UseExceptionHandler(options =>
{

});

app.Run();
