namespace ResidenceMaintenance.Shared.DTO.Rooms;

public class CreateRoomRequest
{
    public int ResidenceId { get; set; }

    public string RoomNumber { get; set; } = string.Empty;

    public int Floor { get; set; }
}