using Packs.Application.Models;

namespace Packs.Application.Repositories;
public interface IPackRepository
{
    Task<bool> CreateAsync(Pack pack);
    Task<Pack?> GetByIdAsync(string id);
    Task<IEnumerable<Pack>> GetAllAsync(GetAllPacksOptions options);
    Task<bool> UpdateAsync(Pack pack);
    Task<bool> ExistsByIdAsync(string id);
    
    // Never delete a pack
    //Task DeleteByIdAsync(string id);
}
