namespace ResidenceMaintenance.Shared.DTO.Rooms;

public class RoomDetails
{
    public int Id { get; set; }

    public int ResidenceId { get; set; }

    public string RoomNumber { get; set; } = string.Empty;

    public int Floor { get; set; }

    public int BedSpaceCount { get; set; }

    public int AvailableBedSpaceCount { get; set; }
}