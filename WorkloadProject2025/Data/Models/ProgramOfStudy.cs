namespace WorkloadProject2025.Data.Models
{
    public class ProgramOfStudy
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Course> Courses { get; set; }
        
        // Additional program information
        public int DurationYears { get; set; }
        public string? Instructor { get; set; }
        public int WorkloadHours { get; set; }
        public decimal Tuition { get; set; }
    }
}
