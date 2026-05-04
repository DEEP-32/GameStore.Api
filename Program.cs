using GameStore.Api.DTOS;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<GameDto> games = [
    new(1,
        "Street Fighter",
        "Fighting game",
        19.99M,
        new DateOnly(1992,7,15)
    ),
    new(2,
        "Street Fighter 2",
        "Fighting game",
        25M,
        new DateOnly(1993,7,15)
    )
];

//GET endpoint /games
app.MapGet("/games", () => games);

// GET endpoint /games/{id}
app.MapGet("/games/{id}", (int id) => games.Find(g => g.Id == id));

//POST endpoint /games


app.Run();