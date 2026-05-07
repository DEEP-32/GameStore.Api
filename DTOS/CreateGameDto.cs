using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOS;

public record CreateGameDto(
    [Required][StringLength(50)] string Name,
    [Range(1,100)]int GenreId,
    [Required][Range(1,100)] decimal Price,
    DateOnly ReleaseDate
);