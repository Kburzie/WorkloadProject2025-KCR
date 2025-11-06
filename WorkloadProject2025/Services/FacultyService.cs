using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;
using WorkloadProject2025.Services.Interfaces;

namespace WorkloadProject2025.Services;

public class FacultyService : IFacultyService
{
    ApplicationDbContext context;

    public FacultyService(ApplicationDbContext db)
    {
        context = db;
    }

    public async Task<Faculty> AddAsync(Faculty faculty, CancellationToken cancellationToken = default)
    {
        if (faculty == null)
            throw new ArgumentNullException();

        if(faculty.Email.Trim() == "")
        {
            throw new Exception("Email must be entered");
        }
        context.Faculty.Add(faculty);

       await context.SaveChangesAsync();

        return faculty;   
        

    }

    public Task<List<Faculty>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.Faculty.ToListAsync(cancellationToken);
    }

    public Task<Faculty?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return context.Faculty.FirstOrDefaultAsync(faculty => faculty.Email == email);
    }

    public async Task<bool> DeleteAsync(Faculty faculty, CancellationToken cancellationToken = default)
    {
        bool result = false;
        try
        {
            context.Faculty.Remove(faculty);
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
