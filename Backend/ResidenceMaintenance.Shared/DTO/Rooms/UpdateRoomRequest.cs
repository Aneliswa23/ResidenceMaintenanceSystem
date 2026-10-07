namespace ResidenceMaintenance.Shared.DTO.Rooms;

public class UpdateRoomRequest
{
    public string RoomNumber { get; set; } = string.Empty;

    public int Floor { get; set; }
}