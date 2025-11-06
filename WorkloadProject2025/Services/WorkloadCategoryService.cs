using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;
using WorkloadProject2025.Services.Interfaces;

namespace WorkloadProject2025.Services;

public class WorkloadCategoryService : IWorkloadCategoryService
{
    ApplicationDbContext _context;

    public WorkloadCategoryService(ApplicationDbContext db)
    {
        _context = db;
    }

    public async Task<WorkloadCategory> AddAsync(WorkloadCategory workloadcategory, CancellationToken cancellationToken = default)
    {
        if (workloadcategory == null)
            throw new ArgumentNullException();

        if (workloadcategory.MinimumHours == 0)
        {
            throw new Exception("Workload must have a minimum amount of hours");
        }
        if (workloadcategory.MaximumHours < workloadcategory.MinimumHours)
        {
            throw new Exception("Workload maximum hours must be higher than minimum hours");
        }
        if (workloadcategory.EndDate <= workloadcategory.StartDate)
        {
            throw new Exception("The End Date must be After the Start Date");
        }
        _context.WorkloadCategories.Add(workloadcategory);
        await _context.SaveChangesAsync();

        return workloadcategory;
    }

    public Task<List<WorkloadCategory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _context.WorkloadCategories.ToListAsync(cancellationToken);
    }

    public Task<WorkloadCategory?> GetByIDAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context.WorkloadCategories.FirstOrDefaultAsync(workloadCategory => workloadCategory.Id == id);
    }

    public async Task<bool> DeleteAsync(WorkloadCategory workloadCategory, CancellationToken cancellationToken = default)
    {
        bool result = false;
        try
        {
            _context.WorkloadCategories.Remove(workloadCategory);
            await _context.SaveChangesAsync();
            result = true;
        }
        catch
        {
            return result;
        }

        return result;
    }
}
