using Microsoft.EntityFrameworkCore;
using ResidenceMaintenance.Core.Interfaces.Services;
using ResidenceMaintenance.Data.Context;
using ResidenceMaintenance.Data.Entities;
using ResidenceMaintenance.Shared.DTO.Rooms;

namespace ResidenceMaintenance.Data.Services;

public class RoomService : IRoomService
{
    private readonly ResidenceMaintenanceDbContext _context;

    public RoomService(ResidenceMaintenanceDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<RoomDetails>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .AsNoTracking()
            .Select(room => new RoomDetails
            {
                Id = room.Id,
                ResidenceId = room.ResidenceId,
                RoomNumber = room.RoomNumber,
                Floor = room.Floor,
                BedSpaceCount = room.BedSpaces.Count,
                AvailableBedSpaceCount = room.BedSpaces
                    .Count(bedSpace => !bedSpace.IsOccupied)
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<RoomDetails>> GetByResidenceIdAsync(
        int residenceId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .AsNoTracking()
            .Where(room => room.ResidenceId == residenceId)
            .Select(room => new RoomDetails
            {
                Id = room.Id,
                ResidenceId = room.ResidenceId,
                RoomNumber = room.RoomNumber,
                Floor = room.Floor,
                BedSpaceCount = room.BedSpaces.Count,
                AvailableBedSpaceCount = room.BedSpaces
                    .Count(bedSpace => !bedSpace.IsOccupied)
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<RoomDetails?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .AsNoTracking()
            .Where(room => room.Id == id)
            .Select(room => new RoomDetails
            {
                Id = room.Id,
                ResidenceId = room.ResidenceId,
                RoomNumber = room.RoomNumber,
                Floor = room.Floor,
                BedSpaceCount = room.BedSpaces.Count,
                AvailableBedSpaceCount = room.BedSpaces
                    .Count(bedSpace => !bedSpace.IsOccupied)
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<RoomDetails> CreateAsync(
        CreateRoomRequest request,
        CancellationToken cancellationToken = default)
    {
        var residenceExists = await _context.Residences
            .AnyAsync(
                residence => residence.Id == request.ResidenceId,
                cancellationToken);

        if (!residenceExists)
        {
            throw new InvalidOperationException(
                $"Residence with ID {request.ResidenceId} does not exist.");
        }

        var room = new Room
        {
            ResidenceId = request.ResidenceId,
            RoomNumber = request.RoomNumber.Trim(),
            Floor = request.Floor
        };

        _context.Rooms.Add(room);

        await _context.SaveChangesAsync(cancellationToken);

        return new RoomDetails
        {
            Id = room.Id,
            ResidenceId = room.ResidenceId,
            RoomNumber = room.RoomNumber,
            Floor = room.Floor,
            BedSpaceCount = 0,
            AvailableBedSpaceCount = 0
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateRoomRequest request,
        CancellationToken cancellationToken = default)
    {
        var room = await _context.Rooms
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (room is null)
        {
            return false;
        }

        room.RoomNumber = request.RoomNumber.Trim();
        room.Floor = request.Floor;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var room = await _context.Rooms
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (room is null)
        {
            return false;
        }

        _context.Rooms.Remove(room);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}