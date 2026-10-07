using Microsoft.AspNetCore.Mvc;
using ResidenceMaintenance.Core.Interfaces.Services;
using ResidenceMaintenance.Shared.DTO.Residences;

namespace ResidenceMaintenance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResidencesController : ControllerBase
{
    private readonly IResidenceService _residenceService;

    public ResidencesController(IResidenceService residenceService)
    {
        _residenceService = residenceService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ResidenceDetails>>> GetAll(
        CancellationToken cancellationToken)
    {
        var residences = await _residenceService.GetAllAsync(cancellationToken);

        return Ok(residences);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResidenceDetails>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var residence = await _residenceService.GetByIdAsync(
            id,
            cancellationToken);

        if (residence is null)
        {
            return NotFound();
        }

        return Ok(residence);
    }

    [HttpPost]
    public async Task<ActionResult<ResidenceDetails>> Create(
        CreateResidenceRequest request,
        CancellationToken cancellationToken)
    {
        var residence = await _residenceService.CreateAsync(
            request,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = residence.Id },
            residence);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateResidenceRequest request,
        CancellationToken cancellationToken)
    {
        var updated = await _residenceService.UpdateAsync(
            id,
            request,
            cancellationToken);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _residenceService.DeleteAsync(
            id,
            cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}