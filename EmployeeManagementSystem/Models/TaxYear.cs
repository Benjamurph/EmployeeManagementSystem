using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class TaxYear : UserActivity
    {
        public int Id { get; set; }

        public string Year { get; set; }

        [Display(Name = "Start Date")]
        public DateOnly StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateOnly EndDate { get; set; }

        public bool Closed { get; set; }

        public bool Locked { get; set; }

        [Display(Name = "Personal Allowance Upper Threshold (£)")]
        public int PersonalAllowance { get; set; }

        [Display(Name = "Basic Rate Lower Threshold (£)")]
        public int BasicRateLowerThreshold { get; set; }

        [Display(Name = "Basic Rate Upper Threshold (£)")]
        public int BasicRateUpperThreshold { get; set; }

        [Display(Name = "Basic Rate Tax Percentage")]
        public int BasicRateTaxPercentage { get; set; }

        [Display(Name = "Higher Rate Lower Threshold (£)")]
        public int HigherRateLowerThreshold { get; set; }

        [Display(Name = "Higher Rate Upper Threshold (£)")]
        public int HigherRateUpperThreshold { get; set; }

        [Display(Name = "Higher Rate Tax Percentage")]
        public int HigherRateTaxPercentage { get; set; }

        [Display(Name = "Additional Rate Lower Threshold (£)")]
        public int AdditionalRateThreshold { get; set; }

        [Display(Name = "Additional Rate Tax Percentage")]
        public int AdditionalRateTaxPercentage { get; set; }
    }
}
