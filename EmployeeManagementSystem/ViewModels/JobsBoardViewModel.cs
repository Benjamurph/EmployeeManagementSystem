using EmployeeManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.ViewModels
{
    public class JobsBoardViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

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

        public List<Department> Departments { get; set; }

        public List<Designation> Designations { get; set; }

        public List<City> Cities { get; set; }

        public List<JobBoard> JobBoards { get; set; }
    }
}
