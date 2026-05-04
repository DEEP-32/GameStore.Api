namespace GameStore.Api.DTOS;


public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);