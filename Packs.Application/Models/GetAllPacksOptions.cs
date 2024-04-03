namespace Packs.Application.Models;

public class GetAllPacksOptions
{
    public string? Id { get; set; }

    public int Page {  get; set; }

    public int PageSize { get; set; }
}
