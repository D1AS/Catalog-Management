using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Packs.Api.Auth;
using Packs.Api.Mapping;
using Packs.Application.Models;
using Packs.Application.Repositories;
using Packs.Application.Services;
using Packs.Contracts.Requests;
using Packs.Contracts.Responses;

namespace Packs.Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
public class PacksController : ControllerBase
{
    private readonly IPackService _packService;
    private readonly IOutputCacheStore _outputCacheStore;

    public PacksController(IPackService packService, IOutputCacheStore outputCacheStore)
    {
        _packService = packService;
        _outputCacheStore = outputCacheStore;
    }

    [HttpPost(ApiEndPoints.Packs.Create)]
    //[Authorize(AuthConstants.AdminUserPolicyName)]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    [ProducesResponseType(typeof(PackResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody]CreatePackRequest request,
        CancellationToken token)
    {
        var pack = request.MapToPack();
        var result = await _packService.CreateAsync(pack);
        if (result is null)
        {
            return BadRequest(new { Pack = pack.Id, Category = pack.Category, Message = "Pack already exists." });
        }
        await _outputCacheStore.EvictByTagAsync("packs", token);
        var packResponse = pack.MapToResponse();
        return CreatedAtAction(nameof(Get), new { id = pack.Id }, packResponse);

    }

    [HttpGet(ApiEndPoints.Packs.Get)]
    [OutputCache(PolicyName = "PackCache")]
    [ProducesResponseType(typeof(PackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute]string id)
    {
        var pack = await _packService.GetByIdAsync(id);
        if (pack is null)
        {
            return NotFound();
        }

        var response = pack.MapToResponse();
        return Ok(response);
    }

    [HttpGet(ApiEndPoints.Packs.GetAll)]
    [OutputCache(PolicyName = "PackCache")]
    [ProducesResponseType(typeof(PacksResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery]GetAllPacksRequest request)
    {
        var options = request.MapToOptions();

        var packs = await _packService.GetAllAsync(options);

        var packsResponse = packs.MapToResponse();
        return Ok(packsResponse);
    }

    [HttpPut(ApiEndPoints.Packs.Update)]
    [Authorize(AuthConstants.TrustedMemberPolicyName)]
    [ProducesResponseType(typeof(PackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] string id,
        [FromBody] UpdatePackRequest request,
        CancellationToken token)
    {
        var pack = request.MapToPack(id);
        var updatedPack = await _packService.UpdateAsync(pack);
        if (updatedPack is null)
        {
            return NotFound();
        }

        await _outputCacheStore.EvictByTagAsync("packs", token);
        var response = updatedPack.MapToResponse();
        return Ok(response);
    }
}
