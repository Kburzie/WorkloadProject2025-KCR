
using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public class ProgramsOfStudyService : IProgramsOfStudyService
    {
        // these lines give access to an instance of the database context
        // injected through dependency injection
        ApplicationDbContext _context;
        public ProgramsOfStudyService(ApplicationDbContext db)
        {
            _context = db;
        }

        public async Task<ProgramOfStudy> AddAsync(ProgramOfStudy program, CancellationToken cancellationToken = default)
        {
            if (program == null)
                throw new ArgumentNullException();
            if (program.Name.Trim() == "")
            {
                throw new Exception("Program must have a name");
            }

            // to add records into a database with EF we just pass in our object and save changes
            _context.ProgramsOfStudy.Add(program);
            // commit to the db
            await _context.SaveChangesAsync();
            // return the record
            return program;
        }

        public Task<List<ProgramOfStudy>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            // Include department for display
            return _context.ProgramsOfStudy.Include(p => p.Department).ToListAsync();
        }

        public Task<ProgramOfStudy?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            //LINQ is a language that lets you write queries in C#
            return _context.ProgramsOfStudy.FirstOrDefaultAsync(program => program.Id == id);
        }

        public Task<List<ProgramOfStudy>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
        {
            // Include related data for the dashboard
            return _context.ProgramsOfStudy
                .Include(p => p.Department)
                    .ThenInclude(d => d.School)
                .Include(p => p.Courses)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var program = await GetByIdAsync(id, cancellationToken);
            if (program != null)
            {
                _context.ProgramsOfStudy.Remove(program);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
