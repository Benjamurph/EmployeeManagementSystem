using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.ViewModels
{
    public class DashboardViewModel
    {
        public int LeaveApplications { get; set; }

        public int Employees { get; set; }

        public int Users { get; set; }

        public int JobApplications { get; set; }

        public List<Announcement> Announcements { get; set; }
    }
}
