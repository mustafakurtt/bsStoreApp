namespace Entities.Dtos;

public record BookDtoForUpdate
{
    public int Id { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }

}