namespace ResidenceMaintenance.Data.Entities;

public class BedSpace
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public required string BedNumber { get; set; }

    public bool IsOccupied { get; set; }

    public Room Room { get; set; } = null!;
}