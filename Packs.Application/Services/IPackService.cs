using Packs.Application.Models;

namespace Packs.Application.Services;
public interface IPackService
{
    Task<bool?> CreateAsync(Pack pack);
    Task<Pack?> GetByIdAsync(string id);
    Task<IEnumerable<Pack>> GetAllAsync(GetAllPacksOptions options);
    Task<Pack?> UpdateAsync(Pack pack);
}
