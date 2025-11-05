using System.ComponentModel.DataAnnotations;

namespace WorkloadProject2025.Data.Models
{
    public class Faculty
    {
        [Key]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Position { get; set; } // Professor, Associate Professor, Assistant Professor, Lecturer
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int? WorkloadCategoryId { get; set; }
        public WorkloadCategory? WorkloadCategory { get; set; }
    }
}
