using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services.Interfaces;

public interface IWorkloadCategoryService
{
    Task<WorkloadCategory> AddAsync(WorkloadCategory workloadcategory, CancellationToken cancellationToken = default);
    Task<List<WorkloadCategory>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<WorkloadCategory?> GetByIDAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(WorkloadCategory workloadcategory, CancellationToken cancellationToken = default);
}
