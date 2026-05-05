using GameStore.Api.Data;
using GameStore.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connectionString = "Data Source=gamestore.db";
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();
app.MapGameEndpoints();

app.MigrateDb();

app.Run();