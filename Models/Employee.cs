using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string Country { get; set; } = "";

        [Required]
        public string State { get; set; } = "";

        [Required]
        public string City { get; set; } = "";

        [Required]
        public string Qualification { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [RegularExpression(@"^[0-9]{10}$")]
        public string PhoneNo { get; set; } = "";
    }
}