namespace ResidenceMaintenance.Shared.DTO.Residences;

public class ResidenceDetails
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Address { get; set; }

    public int RoomCount { get; set; }

    public int BedSpaceCount { get; set; }

    public int AvailableBedSpaceCount { get; set; }
}