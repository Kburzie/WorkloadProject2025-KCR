using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;
using WorkloadProject2025.Services.Interfaces;

namespace WorkloadProject2025.Services;

public class WorkloadTypeService : IWorkloadTypeService
{
    ApplicationDbContext context;

    public WorkloadTypeService(ApplicationDbContext db)
    {
        context = db;
    }

    public async Task<WorkloadType> AddAsync(WorkloadType workloadType, CancellationToken cancellationToken = default)
    {
        if (workloadType == null)
            throw new ArgumentNullException();

        if (workloadType.Name.Trim() == "")
        {
            throw new Exception("Workload type must have a name");
        }
        context.WorkloadTypes.Add(workloadType);
        await context.SaveChangesAsync();
        return workloadType;
    }

    public async Task<bool> DeleteAsync(WorkloadType workloadType, CancellationToken cancellationToken = default)
    {
        bool result = false;
        try
        {
            context.WorkloadTypes.Remove(workloadType);
            await context.SaveChangesAsync();
            result = true;
        }
        catch
        {
            return result;
        }

        return result;
    }

    public Task<List<WorkloadType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.WorkloadTypes.ToListAsync(cancellationToken);
    }

    public Task<WorkloadType?> GetByIDAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.WorkloadTypes.FirstOrDefaultAsync(workloadType => workloadType.Id == id);
    }
}
