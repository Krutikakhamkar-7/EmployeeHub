using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
{
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Date of Birth is required")]
    public DateTime? DOB { get; set; }

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; set; }

    [Required(ErrorMessage = "State is required")]
    public string State { get; set; }

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; }

    [Required(ErrorMessage = "Qualification is required")]
    public string Qualification { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Enter a valid email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone Number is required")]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone Number must be 10 digits")]
    public string PhoneNo { get; set; }
}
}