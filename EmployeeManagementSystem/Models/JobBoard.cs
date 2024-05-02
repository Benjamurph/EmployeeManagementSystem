using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class JobBoard : UserActivity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Sub Heading 1")]
        public string? SubHeading1 { get; set; }

        [Display(Name = "Paragraph 1")]
        public string? Paragraph1 { get; set; }

        [Display(Name = "Sub Heading 2")]
        public string? SubHeading2 { get; set; }

        [Display(Name = "Paragraph 2")]
        public string? Paragraph2 { get; set; }

        [Display(Name = "Sub Heading 3")]
        public string? SubHeading3 { get; set; }

        [Display(Name = "Paragraph 3")]
        public string? Paragraph3 { get; set; }

        [Display(Name = "Sub Heading 4")]
        public string? SubHeading4 { get; set; }

        [Display(Name = "Paragraph 4")]
        public string? Paragraph4 { get; set; }

        [Display(Name = "Sub Heading 5")]
        public string? SubHeading5 { get; set; }

        [Display(Name = "Paragraph 5")]
        public string? Paragraph5 { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        [Display(Name = "Designation")]
        public int DesignationId { get; set; }

        public Designation Designation { get; set; }

        public string Salary { get; set; }

        [Display(Name = "Date Posted")]
        public DateOnly DatePosted { get; set; }

        [Display(Name = "End Date")]
        public DateOnly EndDate { get; set; }

        [Display(Name = "Expiration Status")]
        public int? ExpiredId { get; set; }

        public SystemCodeDetail Expired { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        public City City { get; set; }

        [Display(Name = "Positions Available")]
        public int PositionsAvailable { get; set; }
    }
}
