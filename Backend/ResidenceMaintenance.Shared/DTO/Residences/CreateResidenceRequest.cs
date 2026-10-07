namespace ResidenceMaintenance.Shared.DTO.Residences;

public class CreateResidenceRequest
{
    public string Name { get; set; } = string.Empty;

    public string? Address { get; set; }
}