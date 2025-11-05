using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public class WorkloadService : IWorkloadService
    {
        private readonly ApplicationDbContext _context;
        
        public WorkloadService(ApplicationDbContext db)
        {
            _context = db;
        }

        public async Task<Workload> AddAsync(Workload workload, CancellationToken cancellationToken = default)
        {
            if (workload == null)
                throw new ArgumentNullException(nameof(workload));

            if (string.IsNullOrEmpty(workload.FacultyEmail))
                throw new Exception("Faculty Email is required");

            if (workload.WorkloadCategoryId == 0)
                throw new Exception("Workload Category is required");

            if (workload.TermId == 0)
                throw new Exception("Term is required");

            if (workload.ProgramOfStudyId == 0)
                throw new Exception("Program of Study is required");

            if (workload.Hours <= 0)
                throw new Exception("Hours must be greater than 0");

            // Include relationships for validation
            var category = await _context.WorkloadCategories
                .FirstOrDefaultAsync(w => w.Id == workload.WorkloadCategoryId, cancellationToken);

            if (category != null)
            {
                if (workload.Hours < category.MinimumHours)
                    throw new Exception($"Hours must be at least {category.MinimumHours}");

                if (workload.Hours > category.MaximumHours)
                    throw new Exception($"Hours cannot exceed {category.MaximumHours}");
            }

            _context.Workloads.Add(workload);
            await _context.SaveChangesAsync(cancellationToken);
            return workload;
        }

        public Task<List<Workload>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _context.Workloads
                .Include(w => w.Faculty)
                .Include(w => w.WorkloadCategory)
                .Include(w => w.Term)
                .Include(w => w.ProgramOfStudy)
                .Include(w => w.Course)
                .Where(w => w.IsActive)
                .ToListAsync(cancellationToken);
        }

        public Task<Workload?> GetByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Workloads
                .Include(w => w.Faculty)
                .Include(w => w.WorkloadCategory)
                .Include(w => w.Term)
                .Include(w => w.ProgramOfStudy)
                .Include(w => w.Course)
                .FirstOrDefaultAsync(w => w.Id == id && w.IsActive, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Workload workload, CancellationToken cancellationToken = default)
        {
            try
            {
                // Soft delete by setting IsActive to false
                workload.IsActive = false;
                _context.Workloads.Update(workload);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}