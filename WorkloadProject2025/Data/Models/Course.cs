using System.ComponentModel.DataAnnotations;

namespace WorkloadProject2025.Data.Models;

public class Course
{
    public int Id { get; set; }
    [Required(ErrorMessage = "You must specify a course name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "You must specify course hours")]
    public int? Hours { get; set; }

    [Required(ErrorMessage = "You must select a program")]
    public int? ProgramOfStudyId { get; set; }

    public ProgramOfStudy? ProgramOfStudy { get; set; } = new();
}
