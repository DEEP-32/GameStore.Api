namespace GameStore.Api.DTOS;

public record CreateGameDto(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);