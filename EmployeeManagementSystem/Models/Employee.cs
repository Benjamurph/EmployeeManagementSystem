using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class Employee : UserActivity
    {
        public int Id { get; set; }

        [Display(Name = "Employee Number")]
        public string EmployeeNumber { get; set; }

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

        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        public Country Country { get; set; }

        [Display(Name = "City")]
        public int? CityId { get; set; }

        public City City { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateOnly DateOfBirth { get; set; }

        public string Address { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

        public int? DesignationId { get; set; }

        public Designation Designation { get; set; }

        [Display(Name = "Gender")]
        public int? GenderId { get; set; }

        public SystemCodeDetail Gender { get; set; }

        [Display(Name = "Employment Date")]
        public DateOnly? EmploymentDate { get; set; }

        [Display(Name = "Employment Status")]
        public int? StatusId { get; set; }

        public SystemCodeDetail Status { get; set; }

        [Display(Name = "Inactivity Date")]
        public DateOnly? InactiveDate { get; set; }

        [Display(Name = "Inactivity Reason")]
        public int? InactiveReasonId { get; set; }

        public SystemCodeDetail InactiveReason { get; set; }

        [Display(Name = "Termination Date")]
        public DateOnly? TerminationDate { get; set; }

        [Display(Name = "Termination Reason")]
        public int? TerminationReasonId { get; set; }

        public SystemCodeDetail TerminationReason { get; set; }

        public int? BankId { get; set; }

        public Bank Bank { get; set; }

        [Display(Name = "Bank Account Number")]
        public string? BankAccountNumber { get; set; }

        [Display(Name = "Sort Code")]

        public string? SortCode { get; set; }

        [Display(Name = "Disability")]
        public int? DisabilityId { get; set; }

        public SystemCodeDetail Disability { get; set; }

        [Display(Name = "Employment Terms")]
        public int? EmploymentTermsId { get; set; }

        public SystemCodeDetail EmploymentTerms { get; set; }

        [Display(Name = "Allocated Leave Days")]
        public Decimal? AllocatedLeave { get; set; } = 30;

        [Display(Name = "Leave Balance")]
        public Decimal? LeaveBalance { get; set; } = 30;

        [Display(Name = "Annual Salary (£)")]
        public decimal AnnualSalary { get; set; }

        [Display(Name = "Hourly Rate (£)")]
        public decimal HourlyRate { get; set; }

        [Display(Name = "Contractual Hours Per Week")]
        public decimal ContractualHours { get; set; }

        public string? Photo { get; set; }

        public string? FileName { get; set; }
        [Display(Name = "Employee Photo")]
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
