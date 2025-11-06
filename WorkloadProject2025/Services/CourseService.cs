using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;
using WorkloadProject2025.Services.Interfaces;

namespace WorkloadProject2025.Services;

public class CourseService : ICourseService
{
    ApplicationDbContext context;

    public CourseService(ApplicationDbContext db)
    {
        context = db;
    }

    public async Task<Course> AddAsync(Course course, CancellationToken cancellationToken = default)
    {
        if (course == null)
            throw new ArgumentNullException();

        if (string.IsNullOrWhiteSpace(course.Name))
            throw new Exception("Course must have a name.");

        context.Courses.Add(course);
        await context.SaveChangesAsync(cancellationToken);
        return course;
    }

    public Task<List<Course>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.Courses.ToListAsync(cancellationToken);
    }

    public Task<Course?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Courses.FirstOrDefaultAsync(course => course.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Course course, CancellationToken cancellationToken = default)
    {
        bool result = false;
        try
        {
            context.Courses.Remove(course);
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