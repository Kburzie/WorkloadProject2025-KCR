using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public class FacultyService : IFacultyService
    {
        ApplicationDbContext _context;
        public FacultyService(ApplicationDbContext db)
        {
            _context = db;
        }

        public async Task<Faculty> AddAsync(Faculty faculty, CancellationToken cancellationToken = default)
        {
            if (faculty == null)
                throw new ArgumentNullException();
            if(faculty.Email.Trim() == "")
            {
                throw new Exception("Email must be entered");
            }
            _context.Faculty.Add(faculty);

           await _context.SaveChangesAsync();

            return faculty;   
            

        }

        public Task<List<Faculty>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _context.Faculty.ToListAsync();
            
        }

        public Task<Faculty?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return _context.Faculty.FirstOrDefaultAsync(faculty => faculty.Email == email);
        }

        public async Task DeleteAsync(string email, CancellationToken cancellationToken = default)
        {
            var faculty = await GetByEmailAsync(email, cancellationToken);
            if (faculty != null)
            {
                _context.Faculty.Remove(faculty);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
