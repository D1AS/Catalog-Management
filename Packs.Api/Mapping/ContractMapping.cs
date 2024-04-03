using Packs.Application.Models;
using Packs.Contracts.Requests;
using Packs.Contracts.Responses;

namespace Packs.Api.Mapping;

public static class ContractMapping
{
    public static Pack MapToPack(this CreatePackRequest request)
    {
        return new Pack
        {
            Id = request.Id,
            DisplayName = request.DisplayName,
            Active = request.Active,
            Category = request.Category,
            Categories_listed_in = request.Categories_listed_in.ToList(),
            NewSticker = request.NewSticker,
            NumberOfImages = request.NumberOfImages,
            DefaultImage = request.DefaultImage,
            Paid = request.Paid,
            SortOrderWithinCategory = request.SortOrderWithinCategory,
            StoreProductId = request.StoreProductId,
            PackVersion = request.PackVersion
        };
    }

    public static Pack MapToPack(this UpdatePackRequest request, string id)
    {
        return new Pack
        {
            Id = id,
            DisplayName = request.DisplayName,
            Active = request.Active,
            Category = request.Category,
            Categories_listed_in = request.Categories_listed_in.ToList(),
            NewSticker = request.NewSticker,
            NumberOfImages = request.NumberOfImages,
            DefaultImage = request.DefaultImage,
            Paid = request.Paid,
            SortOrderWithinCategory = request.SortOrderWithinCategory,
            StoreProductId = request.StoreProductId,
            PackVersion = request.PackVersion
        };
    }

    public static PackResponse MapToResponse(this Pack pack)
    {
        return new PackResponse
        {
            Id = pack.Id,
            DisplayName = pack.DisplayName,
            Active = pack.Active,
            Category = pack.Category,
            Categories_listed_in = pack.Categories_listed_in.ToList(),
            NewSticker = pack.NewSticker,
            NumberOfImages = pack.NumberOfImages,
            DefaultImage = pack.DefaultImage,
            Paid = pack.Paid,
            SortOrderWithinCategory = pack.SortOrderWithinCategory,
            StoreProductId = pack.StoreProductId,
            PackVersion = pack.PackVersion
        };
    }

    public static PacksResponse MapToResponse(this IEnumerable<Pack> packs)
    {
        return new PacksResponse
        {
            Items = packs.Select(MapToResponse)
        };
    }

    public static GetAllPacksOptions MapToOptions(this GetAllPacksRequest request)
    {
        return new GetAllPacksOptions
        {
            Id = request.Id,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }

}
