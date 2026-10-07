using ResidenceMaintenance.Shared.DTO.Residences;

namespace ResidenceMaintenance.Core.Interfaces.Services;

public interface IResidenceService
{
    Task<IReadOnlyList<ResidenceDetails>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<ResidenceDetails?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<ResidenceDetails> CreateAsync(
        CreateResidenceRequest request,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(
        int id,
        UpdateResidenceRequest request,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}