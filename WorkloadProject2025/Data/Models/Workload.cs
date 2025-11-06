using System.ComponentModel.DataAnnotations;

namespace WorkloadProject2025.Data.Models;

public class Workload
{
    public int Id { get; set; }

    [Required(ErrorMessage = "You must specify a faculty member")]
    public string? FacultyEmail { get; set; }
    public Faculty Faculty { get; set; }

    [Required(ErrorMessage = "You must specify a workload type")]
    public int? WorkloadTypeId { get; set; }
    public WorkloadType WorkloadType { get; set; }

    [Required(ErrorMessage = "You must specify a workload term")]
    public int? TermId { get; set; }
    public Term Term { get; set; }

    public double Hours { get; set; }

    //Not required in the case of the workload not being coursework (Ex. coordinator or research)
    public int? CourseId { get; set; }
    public Course Course { get; set; }
}
