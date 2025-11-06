using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;
using WorkloadProject2025.Services.Interfaces;

namespace WorkloadProject2025.Services;

public class DepartmentService : IDepartmentService
{
    ApplicationDbContext context;

    public DepartmentService(ApplicationDbContext db)
    {
        context = db;
    }

    public async Task<Department> AddAsync(Department department, CancellationToken cancellationToken = default)
    {
        if (department == null)
            throw new ArgumentNullException();

        if (string.IsNullOrWhiteSpace(department.Name))
            throw new Exception("Department must have a name");

        context.Departments.Add(department);
        await context.SaveChangesAsync(cancellationToken);

        return department;
    }

    public Task<List<Department>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.Departments.ToListAsync(cancellationToken);
    }

    public Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Departments.FirstOrDefaultAsync(department => department.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Department department, CancellationToken cancellationToken = default)
    {
        bool result = false;
        try
        {
            context.Departments.Remove(department);
            await context.SaveChangesAsync();
            result = true;
        }
        catch
        {
            return result;
        }

        return result;
    }
}
