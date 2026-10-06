namespace ResidenceMaintenance.Data.Entities;

public class Room
{
    public int Id { get; set; }

    public int ResidenceId { get; set; }

    public required string RoomNumber { get; set; }

    public int Floor { get; set; }

    public Residence Residence { get; set; } = null!;

    public ICollection<BedSpace> BedSpaces { get; set; } = new List<BedSpace>();
}