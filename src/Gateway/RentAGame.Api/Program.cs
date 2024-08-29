using RentAGame.Catalog;
using RentAGame.Chart;
using RentAGame.Ordering;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddChartModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

var app = builder.Build();

app.Run();
