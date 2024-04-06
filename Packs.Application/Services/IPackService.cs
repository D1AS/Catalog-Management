using Packs.Application.Models;

namespace Packs.Application.Services;
public interface IPackService
{
    Task<bool?> CreateAsync(Pack pack, CancellationToken token = default);
    Task<Pack?> GetByIdAsync(string id, CancellationToken token = default);
    Task<IEnumerable<Pack>> GetAllAsync(GetAllPacksOptions options, CancellationToken token = default);
    Task<Pack?> UpdateAsync(Pack pack, CancellationToken token = default);
}
