namespace Packs.Contracts.Responses;
public class PacksResponse
{
    public required IEnumerable<PackResponse> Items { get; init; } = Enumerable.Empty<PackResponse>();
}
