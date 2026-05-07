using GameStore.Api.DTOS;

namespace GameStore.Api.Models;

public class Game {
    public int Id { get; set; }
    public required string Name { get; set; }
    public Genre? Genre { get; set; }
    public int GenreId { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }

    public void UpdateGameWithDto(UpdateGameDto updateGameDto) {
        Name = updateGameDto.Name;
        GenreId = updateGameDto.GenreId;
        Price = updateGameDto.Price;
        ReleaseDate = updateGameDto.ReleaseDate;
    }
}