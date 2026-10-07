using Microsoft.AspNetCore.Mvc;
using ResidenceMaintenance.Core.Interfaces.Services;
using ResidenceMaintenance.Shared.DTO.Rooms;

namespace ResidenceMaintenance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RoomDetails>>> GetAll(
        CancellationToken cancellationToken)
    {
        var rooms = await _roomService.GetAllAsync(cancellationToken);

        return Ok(rooms);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RoomDetails>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var room = await _roomService.GetByIdAsync(
            id,
            cancellationToken);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpGet("by-residence/{residenceId:int}")]
    public async Task<ActionResult<IReadOnlyList<RoomDetails>>> GetByResidence(
        int residenceId,
        CancellationToken cancellationToken)
    {
        var rooms = await _roomService.GetByResidenceIdAsync(
            residenceId,
            cancellationToken);

        return Ok(rooms);
    }

    [HttpPost]
    public async Task<ActionResult<RoomDetails>> Create(
        CreateRoomRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var room = await _roomService.CreateAsync(
                request,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = room.Id },
                room);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateRoomRequest request,
        CancellationToken cancellationToken)
    {
        var updated = await _roomService.UpdateAsync(
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
        var deleted = await _roomService.DeleteAsync(
            id,
            cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}