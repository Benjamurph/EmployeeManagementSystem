using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }


        [DisplayName("Email Address")]
        public string Email { get; set; }


        [DisplayName("First Name")]
        public string FirstName { get; set; }


        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }


        [DisplayName("Last Name")]
        public string LastName { get; set; }


        [DisplayName("Password")]
        public string Password { get; set; }


        [DisplayName("User Name")]
        public string UserName { get; set; }

        public string? FullName => $"{FirstName} {LastName}";

        public string? RoleId { get; set; }

        [DisplayName("Employee Id")]
        public int? EmployeeId { get; set; }

        public string? Photo { get; set; }

        public string? FileName { get; set; }
        [Display(Name = "Profile Picture")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
