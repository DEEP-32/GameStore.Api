using GameStore.Api.DTOS;

const string EndpointName = "GetGame";

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


app.MapGet("/games/{id}", (int id) => games.Find(g => g.Id == id))
    .WithName(EndpointName);

//POST endpoint /games
app.MapPost("/games", (CreateGameDto newGame) => {
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    
    games.Add(game);

    return Results.CreatedAtRoute(EndpointName, new { id = game.Id }, game);
});

//PUT endpoint /games/id
app.MapPut("/games/{id}", (int id, UpdateGameDto updateGame) => {
    var index = games.FindIndex(g => g.Id == id);
    games[index] = new(
        id, 
        updateGame.Name, 
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate
    );
    return Results.NoContent();
});

app.Run();