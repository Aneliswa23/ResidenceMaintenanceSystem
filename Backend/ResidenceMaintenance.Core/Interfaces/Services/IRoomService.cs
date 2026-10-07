using ResidenceMaintenance.Shared.DTO.Rooms;

namespace ResidenceMaintenance.Core.Interfaces.Services;

public interface IRoomService
{
    Task<IReadOnlyList<RoomDetails>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<RoomDetails>> GetByResidenceIdAsync(
        int residenceId,
        CancellationToken cancellationToken = default);

    Task<RoomDetails?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<RoomDetails> CreateAsync(
        CreateRoomRequest request,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(
        int id,
        UpdateRoomRequest request,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}