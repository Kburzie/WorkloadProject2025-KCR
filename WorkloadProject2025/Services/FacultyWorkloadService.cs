using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public class FacultyWorkloadService : IFacultyWorkloadService
    {
        private readonly ApplicationDbContext _context;

        public FacultyWorkloadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FacultyWorkload>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.FacultyWorkloads
                .Include(fw => fw.Faculty)
                    .ThenInclude(f => f.Department)
                .Include(fw => fw.Course)
                    .ThenInclude(c => c.ProgramOfStudy)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<FacultyWorkload>> GetByFacultyEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.FacultyWorkloads
                .Include(fw => fw.Faculty)
                    .ThenInclude(f => f.Department)
                .Include(fw => fw.Course)
                    .ThenInclude(c => c.ProgramOfStudy)
                .Where(fw => fw.FacultyEmail == email)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<FacultyWorkload>> GetBySemesterAsync(string semester, int year, CancellationToken cancellationToken = default)
        {
            return await _context.FacultyWorkloads
                .Include(fw => fw.Faculty)
                    .ThenInclude(f => f.Department)
                .Include(fw => fw.Course)
                    .ThenInclude(c => c.ProgramOfStudy)
                .Where(fw => fw.Semester == semester && fw.Year == year)
                .ToListAsync(cancellationToken);
        }

        public async Task<FacultyWorkload> AddAsync(FacultyWorkload workload, CancellationToken cancellationToken = default)
        {
            if (workload == null)
                throw new ArgumentNullException(nameof(workload));

            if (string.IsNullOrWhiteSpace(workload.FacultyEmail))
                throw new ArgumentException("Faculty email is required", nameof(workload));

            // Verify faculty exists
            var facultyExists = await _context.Faculty.AnyAsync(f => f.Email == workload.FacultyEmail, cancellationToken);
            if (!facultyExists)
                throw new InvalidOperationException($"Faculty with email '{workload.FacultyEmail}' does not exist");

            _context.FacultyWorkloads.Add(workload);
            await _context.SaveChangesAsync(cancellationToken);

            return workload;
        }

        public async Task<List<FacultyWorkload>> CopyWorkloadToSemesterAsync(string fromSemester, int fromYear, string toSemester, int toYear, CancellationToken cancellationToken = default)
        {
            var sourceWorkloads = await _context.FacultyWorkloads
                .Where(fw => fw.Semester == fromSemester && fw.Year == fromYear)
                .ToListAsync(cancellationToken);

            var copiedWorkloads = new List<FacultyWorkload>();

            foreach (var source in sourceWorkloads)
            {
                var copied = new FacultyWorkload
                {
                    FacultyEmail = source.FacultyEmail,
                    Type = source.Type,
                    CourseId = source.CourseId,
                    DeliveryType = source.DeliveryType,
                    HoursPerWeek = source.HoursPerWeek,
                    TotalStudents = source.TotalStudents,
                    CoordinationRole = source.CoordinationRole,
                    ProjectName = source.ProjectName,
                    ProjectHours = source.ProjectHours,
                    Semester = toSemester,
                    Year = toYear,
                    StartDate = DateTime.UtcNow
                };

                _context.FacultyWorkloads.Add(copied);
                copiedWorkloads.Add(copied);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return copiedWorkloads;
        }

        public async Task<List<FacultyWorkload>> CopyProgramCoursesToWorkloadAsync(int programId, string semester, int year, string facultyEmail, CancellationToken cancellationToken = default)
        {
            var courses = await _context.Courses
                .Where(c => c.ProgramOfStudyId == programId)
                .ToListAsync(cancellationToken);

            var workloads = new List<FacultyWorkload>();

            foreach (var course in courses)
            {
                var workload = new FacultyWorkload
                {
                    FacultyEmail = facultyEmail,
                    Type = WorkloadType.course,
                    CourseId = course.Id,
                    DeliveryType = "Lecture",
                    HoursPerWeek = course.Hours,
                    Semester = semester,
                    Year = year,
                    StartDate = DateTime.UtcNow
                };

                _context.FacultyWorkloads.Add(workload);
                workloads.Add(workload);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return workloads;
        }
    }
}
