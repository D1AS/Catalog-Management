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

    public async Task<bool?> CreateAsync(Pack pack, CancellationToken token = default)
    {
        await _packValidator.ValidateAndThrowAsync(pack, cancellationToken: token);

        var exists = await _packRepository.ExistsByIdAsync(pack.Id);
        if (exists)
        {
            return null;
        }

        return await _packRepository.CreateAsync(pack);
    }

    public Task<IEnumerable<Pack>> GetAllAsync(GetAllPacksOptions options, CancellationToken token = default)
    {
        return _packRepository.GetAllAsync(options);
    }

    public Task<Pack?> GetByIdAsync(string id, CancellationToken token = default)
    {
        return _packRepository.GetByIdAsync(id);
    }

    public async Task<Pack?> UpdateAsync(Pack pack, CancellationToken token = default)
    {
        await _packValidator.ValidateAndThrowAsync(pack, cancellationToken: token);

        var exists = await _packRepository.ExistsByIdAsync(pack.Id);
        if (!exists)
        {
            return null;
        }

        await _packRepository.UpdateAsync(pack);

        return pack;
    }
}
