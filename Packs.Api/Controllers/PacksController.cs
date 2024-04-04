using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    public PacksController(IPackService packService)
    {
        _packService = packService;
    }

    [HttpPost(ApiEndPoints.Packs.Create)]
    [Authorize(AuthConstants.AdminUserPolicyName)]
    [ProducesResponseType(typeof(PackResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody]CreatePackRequest request)
    {
        var pack = request.MapToPack();
        var result = await _packService.CreateAsync(pack);
        if (result is null)
        {
            return BadRequest(new { Pack = pack.Id, Category = pack.Category, Message = "Pack already exists." });
        }

        return CreatedAtAction(nameof(Get), new { id = pack.Id }, pack);
    }

    [HttpGet(ApiEndPoints.Packs.Get)]
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
        [FromBody] UpdatePackRequest request)
    {
        var pack = request.MapToPack(id);
        var updatedPack = await _packService.UpdateAsync(pack);
        if (updatedPack is null)
        {
            return NotFound();
        }

        var response = updatedPack.MapToResponse();
        return Ok(response);
    }
}
