var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarterModulesFromAssemblies
(
    typeof(CreateGameEndpoint).Assembly
// Chart module assembly gelecek
// Ordering module assembly gelecek
);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddChartModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

var app = builder.Build();

// Carter paketini kullanarak route map işlemleri icra edilecek
app.MapCarter();

app
    .UseCatalogModule()
    .UseChartModule()
    .UseOrderingModule();

app.Run();
