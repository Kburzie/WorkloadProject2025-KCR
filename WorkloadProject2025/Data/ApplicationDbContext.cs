using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data.Models;
using static WorkloadProject2025.Components.Pages.WorkloadPage;

namespace WorkloadProject2025.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<School> Schools { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<WorkloadCategory> WorkloadCategories { get; set; }
        public DbSet<ProgramOfStudy> ProgramsOfStudy { get; set; }
        public DbSet<FacultyWorkload> FacultyWorkloads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // School-Department relationship
            modelBuilder.Entity<Department>()
                .HasOne(d => d.School)
                .WithMany(s => s.Departments)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.Cascade);

            // Department-ProgramOfStudy relationship
            modelBuilder.Entity<ProgramOfStudy>()
                .HasOne(p => p.Department)
                .WithMany(d => d.ProgramsOfStudy)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProgramOfStudy-Course relationship
            modelBuilder.Entity<Course>()
                .HasOne(c => c.ProgramOfStudy)
                .WithMany(p => p.Courses)
                .HasForeignKey(c => c.ProgramOfStudyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Faculty-Department relationship
            modelBuilder.Entity<Faculty>()
                .HasOne(f => f.Department)
                .WithMany()
                .HasForeignKey(f => f.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            // Faculty-WorkloadCategory relationship
            modelBuilder.Entity<Faculty>()
                .HasOne(f => f.WorkloadCategory)
                .WithMany()
                .HasForeignKey(f => f.WorkloadCategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // FacultyWorkload-Faculty relationship
            modelBuilder.Entity<FacultyWorkload>()
                .HasOne(fw => fw.Faculty)
                .WithMany()
                .HasForeignKey(fw => fw.FacultyEmail)
                .HasPrincipalKey(f => f.Email)
                .OnDelete(DeleteBehavior.Cascade);

            // FacultyWorkload-Course relationship
            modelBuilder.Entity<FacultyWorkload>()
                .HasOne(fw => fw.Course)
                .WithMany()
                .HasForeignKey(fw => fw.CourseId)
                .OnDelete(DeleteBehavior.SetNull);

            

            base.OnModelCreating(modelBuilder);
        }
    }
}
