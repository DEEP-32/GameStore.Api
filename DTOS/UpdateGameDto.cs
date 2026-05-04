namespace GameStore.Api.DTOS;

public record UpdateGameDto(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);