using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;
using WorkloadProject2025.Services.Interfaces;

namespace WorkloadProject2025.Services;

public class SchoolService : ISchoolService
{
    ApplicationDbContext context;

    public SchoolService(ApplicationDbContext db)
    {
        context = db;
    }

    public async Task<School> AddAsync(School school, CancellationToken cancellationToken = default)
    {
        if (school == null)
            throw new ArgumentNullException();

        if(school.Name.Trim() == "")
        {
            throw new Exception("School must have a name");
        }
        context.Schools.Add(school);
        await context.SaveChangesAsync();
        return school;
    }

    public Task<List<School>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.Schools.ToListAsync(cancellationToken);
    }

    public Task<School?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Schools.FirstOrDefaultAsync(school => school.Id == id);
    }

    public async Task<bool> DeleteAsync(School school, CancellationToken cancellationToken = default)
    {
        bool result = false;
        try
        {
            context.Schools.Remove(school);
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
