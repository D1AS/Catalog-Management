using FluentValidation;
using Packs.Application.Models;
using Packs.Application.Repositories;

namespace Packs.Application.Services;
public class PackService : IPackService
{
    private readonly IPackRepository _packRepository;
    private readonly IValidator<Pack> _packValidator;

    public PackService(IPackRepository packRepository, IValidator<Pack> packValidator)
    {
        _packRepository = packRepository;
        _packValidator = packValidator;
    }

    public async Task<bool?> CreateAsync(Pack pack)
    {
        var exists = await _packRepository.ExistsByIdAsync(pack.Id);
        if (exists)
        {
            return null;
        }

        return await _packRepository.CreateAsync(pack);
    }

    public Task<IEnumerable<Pack>> GetAllAsync(GetAllPacksOptions options)
    {
        return _packRepository.GetAllAsync(options);
    }

    public Task<Pack?> GetByIdAsync(string id)
    {
        return _packRepository.GetByIdAsync(id);
    }

    public async Task<Pack?> UpdateAsync(Pack pack)
    {
        var exists = await _packRepository.ExistsByIdAsync(pack.Id);
        if (!exists)
        {
            return null;
        }

        await _packRepository.UpdateAsync(pack);

        return pack;
    }
}
