namespace WorkloadProject2025.Data.Models
{

    public enum WorkloadType
    {
        course,
        Coordination,
        Project
    }
    public class FacultyWorkload
    {
        public int Id { get; set; }

        public string FacultyEmail { get; set; } = string.Empty;   // Faculty Email as foreign key
        public Faculty? Faculty { get; set; }
        public WorkloadType Type { get; set; }

        // For Course Workload
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
        public string? DeliveryType { get; set; } // Lecture, Lab, etc.
        public decimal? HoursPerWeek { get; set; }
        public int? TotalStudents { get; set; }

        // For Coordinator
        public string? CoordinationRole { get; set; }

        // For Project
        public string? ProjectName { get; set; }
        public decimal? ProjectHours { get; set; }

        // Common
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        
        // Semester tracking
        public string? Semester { get; set; } // e.g., "Fall 2024", "Spring 2025"
        public int? Year { get; set; }

    }
}
