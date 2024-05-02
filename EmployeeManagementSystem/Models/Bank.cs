using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Bank : UserActivity
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }
    }
}
