namespace Packs.Contracts.Requests;
public class GetAllPacksRequest : PagedRequest
{
    public required string? Id { get; init; }
}
