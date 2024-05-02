using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class JobApplication : UserActivity
    {
        public int Id { get; set; }

        public int JobPostId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]

        public string? MiddleName { get; set; }

        [Display(Name = "Last Name")]

        public string LastName { get; set; }

        [Display(Name = "Full name")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Display(Name = "Gender")]
        public int? GenderId { get; set; }

        public SystemCodeDetail Gender { get; set; }

        [Display(Name = "Disability")]
        public int? DisabilityId { get; set; }

        public SystemCodeDetail Disability { get; set; }

        [Display(Name = "Approval Status")]
        public int StatusId { get; set; }
        public SystemCodeDetail Status { get; set; }

        public string? CV { get; set; }

        public string? FileName { get; set; }
        [Display(Name = "CV")]
        [NotMapped]
        public IFormFile? CVFile { get; set; }
    }
}
