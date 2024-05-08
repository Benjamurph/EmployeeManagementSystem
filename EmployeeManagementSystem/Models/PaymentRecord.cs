using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class PaymentRecord : UserActivity
    {
        public int Id { get; set; }

        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Display(Name = "PayDate")]
        public DateTime PayDate { get; set; } = DateTime.Now;

        [Display(Name = "Tax Year")]
        public int TaxYearId { get; set; }

        public TaxYear TaxYear { get; set; }

        [Display(Name = "Contractual Hours")]
        public decimal ContractualHours { get; set; }

        [Display(Name = "Overtime Hours")]
        public decimal OvertimeHours { get; set; }

        [Display(Name = "Total Hours Worked")]
        public decimal TotalHoursWorked { get; set; }

        [Display(Name = "Contractual Earnings")]
        public decimal ContractualEarnings { get; set; }

        [Display(Name = "Overtime Earnings")]
        public decimal OvertimeEarnings { get; set; }

        [Display(Name = "Tax Rate")]
        public decimal TaxRate { get; set; }

        public decimal NIC { get; set; }

        [Display(Name = "Total Earnings")]
        public decimal TotalEarnings { get; set; }

        [Display(Name = "Total Deduction")]
        public decimal TotalDeduction { get; set; }

        [Display(Name = "Net Payment")]
        public decimal NetPayment { get; set; }
    }
}
