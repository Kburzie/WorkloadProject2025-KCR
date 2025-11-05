namespace WorkloadProject2025.Data.Models
{
    public class CopyWorkloadData
    {
        public string FromSemester { get; set; } = "";
        public int FromYear { get; set; }
        public string ToSemester { get; set; } = "";
        public int ToYear { get; set; }
    }

    public class CopyProgramData
    {
        public int ProgramId { get; set; }
        public string FacultyEmail { get; set; } = "";
        public string Semester { get; set; } = "";
        public int Year { get; set; }
    }
}
