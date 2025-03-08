using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only alphabets and spaces are allowed.")]
        [MinLength(15, ErrorMessage = "Name must be at least 15 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Date of Joining is required.")]
        public DateTime DateOfJoining { get; set; }

        [Range(12000, 60000, ErrorMessage = "Salary must be between 12000 and 60000.")]
        public double? Salary { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [RegularExpression(@"^(HR|Accts|IT)$", ErrorMessage = "Department must be HR, Accts, or IT.")]
        public string Dept { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
