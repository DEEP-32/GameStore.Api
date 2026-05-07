using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOS;

public record UpdateGameDto(
    string Name,
    [Range(1,100)]int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);