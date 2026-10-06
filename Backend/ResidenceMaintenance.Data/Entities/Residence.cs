namespace ResidenceMaintenance.Data.Entities;

public class Residence
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Address { get; set; }

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}