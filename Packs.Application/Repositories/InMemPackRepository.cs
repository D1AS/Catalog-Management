using Packs.Application.Models;
using System.Collections.Generic;

namespace Packs.Application.Repositories;
public class InMemPackRepository : IPackRepository
{

    private readonly List<string> categories = new()
    {
            "On Sale",
            "New",
            "Animals",
            "Art",
            "Colorful",
            "Food",
            "Lifestyle",
            "Nature",
            "Seasons",
            "Travel",
            "Other"
        };

    private readonly List<Pack> _packs = new()
    {
        new Pack()
        {
            Id = "nature",
            DisplayName ="Nature",
            Active = true,
            Category = "Nature",
            Categories_listed_in = new List<string> { "Nature", "New" },
            NewSticker = true,
            NumberOfImages = 30,
            DefaultImage = 30,
            Paid = true,
            SortOrderWithinCategory = 1,
            StoreProductId = "nature.rgllc",
            PackVersion = 1
        },
        new Pack()
        {
            Id = "tasty_food",
            DisplayName ="Tasty Food",
            Active = true,
            Category = "Food",
            Categories_listed_in = new List<string> { "Food", "New" },
            NewSticker = true,
            NumberOfImages = 30,
            DefaultImage = 23,
            Paid = true,
            SortOrderWithinCategory = 1,
            StoreProductId = "tasty_food.rgllc",
            PackVersion = 1
        },
        new Pack()
        {
            Id = "art",
            DisplayName ="Art",
            Active = true,
            Category = "Art",
            Categories_listed_in = new List<string> { "Art" },
            NewSticker = false,
            NumberOfImages = 10,
            DefaultImage = 1,
            Paid = false,
            SortOrderWithinCategory = 1,
            StoreProductId = "art.rgllc",
            PackVersion = 1
        }
    };

    public Task<bool> CreateAsync(Pack pack)
    {
        _packs.Add(pack);
        return Task.FromResult(true);
    }

    public Task<bool> ExistsByIdAsync(string id)
    {
        var result = _packs.Exists(x => x.Id == id);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<Pack>> GetAllAsync(GetAllPacksOptions options)
    {
        var query = _packs.AsEnumerable();

        if (options.Id != null)
        {
            query = query.Where(p => p.Id.Contains(options.Id));
        }

        return Task.FromResult(_packs.Skip((options.Page - 1) * options.PageSize).Take(options.PageSize).AsEnumerable());
    }

    public Task<Pack?> GetByIdAsync(string id)
    {
        var pack = _packs.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(pack);
    }

    public Task<bool> UpdateAsync(Pack pack)
    {
        var packIndex = _packs.FindIndex(x => x.Id == pack.Id);
        if (packIndex == -1)
        {
            return Task<bool>.FromResult(false);
        }

        _packs[packIndex] = pack;
        return Task.FromResult(true);
    }
}
