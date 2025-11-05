using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;
using WorkloadProject2025.Services.Interfaces;

namespace WorkloadProject2025.Services;

public class TermService : ITermService
{
    ApplicationDbContext _context;

    public TermService(ApplicationDbContext db)
    {
        _context = db;
    }

    public async Task<Term> AddAsync(Term intake, CancellationToken cancellationToken = default)
    {
        if (intake == null)
            throw new ArgumentNullException();

        if (string.IsNullOrWhiteSpace(intake.Name))
            throw new ArgumentException("Intake must have a name", nameof(intake));

        if (intake.EndDate <= intake.StartDate)
            throw new ArgumentException("End date must be after start date");

        _context.Terms.Add(intake);
        await _context.SaveChangesAsync(cancellationToken);
        return intake;
        
    }

    public Task<List<Term>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _context.Terms.ToListAsync();
    }

    public Task<Term?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context.Terms.FirstOrDefaultAsync(intake => intake.Id == id);
    }

    public async Task<bool> DeleteAsync(Term term, CancellationToken cancellationToken = default)
    {
        bool result = false;
        try
        {
            _context.Terms.Remove(term);
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
