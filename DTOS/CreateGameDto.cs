using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOS;

public record CreateGameDto(
    [Required][StringLength(50)] string Name,
    string Genre,
    [Required][Range(1,100)] decimal Price,
    DateOnly ReleaseDate
);