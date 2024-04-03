namespace Packs.Contracts.Responses;
public class PackResponse
{
    public required string Id { get; init; }
    public required string DisplayName { get; init; }
    public required bool Active { get; init; }
    public required string Category { get; init; }
    public required IEnumerable<string> Categories_listed_in { get; init; } = Enumerable.Empty<string>();
    public required bool NewSticker { get; init; }
    public required int NumberOfImages { get; init; }
    public required int DefaultImage { get; init; }
    public required bool Paid { get; init; }
    public required int SortOrderWithinCategory { get; init; }
    public required string StoreProductId { get; init; }
    public required int PackVersion { get; init; }
}
