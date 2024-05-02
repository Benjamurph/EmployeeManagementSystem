using EmployeeManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Employee Number")]
        public string EmployeeNumber { get; set; }

        [Display(Name = "Employee First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Employee Middle Name")]

        public string? MiddleName { get; set; }

        [Display(Name = "Employee Last Name")]

        public string LastName { get; set; }

        [Display(Name = "Employee Full name")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Phone Number")]

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        [Display(Name = "City")]
        public int? CityId { get; set; }

        [Display(Name = "Date Of Birth")]

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public int? DepartmentId { get; set; }

        public int? DesignationId { get; set; }


        [Display(Name = "Gender")]
        public int? GenderId { get; set; }


        [Display(Name = "Employee Photo")]
        public string? Photo { get; set; }

        [Display(Name = "Employment Date")]
        public DateTime? EmploymentDate { get; set; }

        public int? StatusId { get; set; }


        [Display(Name = "Inactivity Date")]
        public DateTime? InactiveDate { get; set; }

        [Display(Name = "Inactivity Reason")]
        public int? InactiveReasonId { get; set; }

        [Display(Name = "Termination Date")]
        public DateTime? TerminationDate { get; set; }

        [Display(Name = "Termination Reason")]
        public int? TerminationReasonId { get; set; }

        [Display(Name = "Bank")]
        public int? BankId { get; set; }


        [Display(Name = "Bank Account Number")]
        public string? BankAccountNumber { get; set; }

        [Display(Name = "Sort Code")]

        public string? SortCode { get; set; }

        [Display(Name = "Disability")]
        public int? DisabilityId { get; set; }


        [Display(Name = "Employment Terms")]
        public int? EmploymentTermsId { get; set; }


        [Display(Name = "Allocated Leave Days")]
        public Decimal? AllocatedLeave { get; set; } = 30;

        [Display(Name = "Leave Balance")]
        public Decimal? LeaveBalance { get; set; } = 30;

        public Employee Employee { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
