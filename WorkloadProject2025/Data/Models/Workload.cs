using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkloadProject2025.Data.Models
{
    public class Workload
    {
        public int Id { get; set; }

        [Required]
        public string FacultyEmail { get; set; }
        [ForeignKey("FacultyEmail")]
        public Faculty Faculty { get; set; }

        [Required]
        public int WorkloadCategoryId { get; set; }
        [ForeignKey("WorkloadCategoryId")]
        public WorkloadCategory WorkloadCategory { get; set; }

        [Required]
        public int TermId { get; set; }
        [ForeignKey("TermId")]
        public Term Term { get; set; }

        [Required]
        public int ProgramOfStudyId { get; set; }
        [ForeignKey("ProgramOfStudyId")]
        public ProgramOfStudy ProgramOfStudy { get; set; }

        public int? CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Hours { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime DateAssigned { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}

