using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;
using WorkloadProject2025.Services.Interfaces;

namespace WorkloadProject2025.Services;

public class ProgramOfStudyService : IProgramOfStudyService
{
    ApplicationDbContext context;

    public ProgramOfStudyService(ApplicationDbContext db)
    {
        context = db;
    }

    public async Task<ProgramOfStudy> AddAsync(ProgramOfStudy program, CancellationToken cancellationToken = default)
    {
        if (program == null)
            throw new ArgumentNullException();

        if (program.Name.Trim() == "")
        {
            throw new Exception("Program must have a name");
        }

        context.ProgramsOfStudy.Add(program);
        await context.SaveChangesAsync();
        return program;
    }

    public Task<List<ProgramOfStudy>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.ProgramsOfStudy.ToListAsync(cancellationToken);
    }

    public Task<ProgramOfStudy?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.ProgramsOfStudy.FirstOrDefaultAsync(programOfStudy => programOfStudy.Id == id);
    }

    public async Task<bool> DeleteAsync(ProgramOfStudy programOfStudy, CancellationToken cancellationToken = default)
    {
        bool result = false;
        try
        {
            context.ProgramsOfStudy.Remove(programOfStudy);
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
