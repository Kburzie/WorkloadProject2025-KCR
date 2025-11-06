using System.ComponentModel.DataAnnotations;

namespace WorkloadProject2025.Data.Models;

public class Faculty
{
    [Key]
    public string Email { get; set; }

    [Required(ErrorMessage = "You must enter a first name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "You must enter a last name")]
    public string LastName { get; set; }

    public string PhoneNumber { get; set; }
}
