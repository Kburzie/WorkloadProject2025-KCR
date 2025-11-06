using System.ComponentModel.DataAnnotations;

namespace WorkloadProject2025.Data.Models;

public class WorkloadCategory
{
    public int Id { get; set; }

    public int MinimumHours { get; set; }

    public int MaximumHours { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

}
