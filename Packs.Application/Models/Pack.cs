namespace Packs.Application.Models;
public class Pack
{
    public required string Id { get; set; }
    public required string DisplayName { get; set; }
    public required bool Active { get; set; }
    public required string Category { get; set; }
    public required List<string> Categories_listed_in { get; set; } = new List<string>();
    public required bool NewSticker { get; set; }
    public required int NumberOfImages { get; set; }
    public required int DefaultImage { get; set; }
    public required bool Paid { get; set; }
    public required int SortOrderWithinCategory { get; set; }
    public required string StoreProductId { get; set; }
    public required int PackVersion { get; set; }
}
