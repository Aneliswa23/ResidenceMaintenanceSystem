using Microsoft.EntityFrameworkCore;
using ResidenceMaintenance.Core.Interfaces.Services;
using ResidenceMaintenance.Data.Context;
using ResidenceMaintenance.Data.Entities;
using ResidenceMaintenance.Shared.DTO.Residences;

namespace ResidenceMaintenance.Data.Services;

public class ResidenceService : IResidenceService
{
    private readonly ResidenceMaintenanceDbContext _context;

    public ResidenceService(ResidenceMaintenanceDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ResidenceDetails>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Residences
            .AsNoTracking()
            .Select(residence => new ResidenceDetails
            {
                Id = residence.Id,
                Name = residence.Name,
                Address = residence.Address,
                RoomCount = residence.Rooms.Count,
                BedSpaceCount = residence.Rooms
                    .SelectMany(room => room.BedSpaces)
                    .Count(),
                AvailableBedSpaceCount = residence.Rooms
                    .SelectMany(room => room.BedSpaces)
                    .Count(bedSpace => !bedSpace.IsOccupied)
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ResidenceDetails?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Residences
            .AsNoTracking()
            .Where(residence => residence.Id == id)
            .Select(residence => new ResidenceDetails
            {
                Id = residence.Id,
                Name = residence.Name,
                Address = residence.Address,
                RoomCount = residence.Rooms.Count,
                BedSpaceCount = residence.Rooms
                    .SelectMany(room => room.BedSpaces)
                    .Count(),
                AvailableBedSpaceCount = residence.Rooms
                    .SelectMany(room => room.BedSpaces)
                    .Count(bedSpace => !bedSpace.IsOccupied)
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ResidenceDetails> CreateAsync(
        CreateResidenceRequest request,
        CancellationToken cancellationToken = default)
    {
        var residence = new Residence
        {
            Name = request.Name.Trim(),
            Address = string.IsNullOrWhiteSpace(request.Address)
                ? null
                : request.Address.Trim()
        };

        _context.Residences.Add(residence);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResidenceDetails
        {
            Id = residence.Id,
            Name = residence.Name,
            Address = residence.Address,
            RoomCount = 0,
            BedSpaceCount = 0,
            AvailableBedSpaceCount = 0
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateResidenceRequest request,
        CancellationToken cancellationToken = default)
    {
        var residence = await _context.Residences
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (residence is null)
        {
            return false;
        }

        residence.Name = request.Name.Trim();
        residence.Address = string.IsNullOrWhiteSpace(request.Address)
            ? null
            : request.Address.Trim();

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var residence = await _context.Residences
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (residence is null)
        {
            return false;
        }

        _context.Residences.Remove(residence);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}