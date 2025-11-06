using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services.Interfaces;

public interface IWorkloadTypeService
{
    Task<WorkloadType> AddAsync(WorkloadType workloadType, CancellationToken cancellationToken = default);
    Task<List<WorkloadType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<WorkloadType?> GetByIDAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(WorkloadType workloadType, CancellationToken cancellationToken = default);
}
