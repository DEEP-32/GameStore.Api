using GameStore.Api.Data;
using GameStore.Api.DTOS;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints {
    const string EndpointName = "GetGame";
    const string RoutRoot = "/games";

    public static void MapGameEndpoints(this WebApplication app) {

        var group = app.MapGroup("/games");
        
        //GET endpoint /games
        group.MapGet("/", async (GameStoreContext dbContext) 
            => await dbContext.Games
                .Include(game => game.Genre)
                .Select(game => new GameSummaryDto(
                    game.Id,
                    game.Name,
                    game.Genre!.Name,
                    game.Price,
                    game.ReleaseDate
                ))
                .AsNoTracking()
                .ToListAsync()
        );

        // GET endpoint /games/{id}


        group.MapGet("/{id}", async (int id,GameStoreContext dbContext) => {
                var game = await dbContext.Games.FindAsync(id);
                return game == null ? Results.NotFound() : Results.Ok(
                        new GameDetailsDto (
                            game.Id,
                            game.Name,
                            game.GenreId,
                            game.Price,
                            game.ReleaseDate
                        )
                    );
            })
            .WithName(EndpointName);


        //POST endpoint /games
        group.MapPost("/", async (CreateGameDto newGame,GameStoreContext dbContext) => {
            Game game = new(){
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDetailsDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute(EndpointName, new { id = gameDetailsDto.Id }, gameDetailsDto);
        });

        //PUT endpoint /games/id
        group.MapPut("/{id}", 
            async (int id, UpdateGameDto updateGame,GameStoreContext dbContext) => {
            var exsistingGame = await dbContext.Games.FindAsync(id);

            if (exsistingGame is null) {
                return Results.NotFound();
            }

            exsistingGame.UpdateGameWithDto(updateGame);
            await dbContext.SaveChangesAsync();
            
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id,GameStoreContext dbContext) => {
            await dbContext.Games
                .Where(game => game.Id == id)
                .ExecuteDeleteAsync();
            
            return Results.NoContent();
        });
    }
}