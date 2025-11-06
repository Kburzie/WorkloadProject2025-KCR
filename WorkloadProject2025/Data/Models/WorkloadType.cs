using System.ComponentModel.DataAnnotations;

namespace WorkloadProject2025.Data.Models;

public class WorkloadType
{
    public int Id { get; set; }

    [Required(ErrorMessage = "You must specify a workload type")]
    public string Name { get; set; }
}
