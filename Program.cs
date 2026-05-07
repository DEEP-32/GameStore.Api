using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.SeedGenreDb();


var app = builder.Build();
app.MapGameEndpoints();
app.MapGenresEndpoints();

app.MigrateDb();

app.Run();