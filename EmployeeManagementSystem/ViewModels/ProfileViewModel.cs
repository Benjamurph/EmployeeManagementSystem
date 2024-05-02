using EmployeeManagementSystem.Models;
using System.ComponentModel;

namespace EmployeeManagementSystem.ViewModels
{
    public class ProfileViewModel
    {
        public ICollection<SystemProfile> Profiles { get; set; }

        public ICollection<int> RolesRightsIds { get; set; }
        public int[] Ids { get; set; }

        [DisplayName("Role")]
        public string RoleId { get; set; }

        [DisplayName("System Task")]
        public int TaskId { get; set; }
    }
}
