using GameStore.Api.DTOS;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints {
    const string EndpointName = "GetGame";
    const string RoutRoot = "/games";

    private readonly static List<GameDto> games = [
        new(1,
            "Street Fighter",
            "Fighting game",
            19.99M,
            new DateOnly(1992, 7, 15)
        ),
        new(2,
            "Street Fighter 2",
            "Fighting game",
            25M,
            new DateOnly(1993, 7, 15)
        )
    ];

    public static void MapGameEndpoints(this WebApplication app) {

        var group = app.MapGroup("/games");
        
        //GET endpoint /games
        group.MapGet("/", () => games);

        // GET endpoint /games/{id}


        group.MapGet("/{id}", (int id) => {
                var game = games.Find(g => g.Id == id);
                return game is null ? Results.NotFound() : Results.Ok(game);
            })
            .WithName(EndpointName);


        //POST endpoint /games
        group.MapPost("/", (CreateGameDto newGame) => {
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
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) => {
            var index = games.FindIndex(g => g.Id == id);

            if (index == -1) {
                return Results.NotFound();
            }

            games[index] = new(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.ReleaseDate
            );
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(g => g.Id == id);
            return Results.NoContent();
        });
    }
}