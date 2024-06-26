﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class LeaveApplication : ApprovalActivity
    {
        public int Id { get; set; }

        [Display(Name = "Employee Name")]
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        [Display(Name = "Requested Leave Days")]
        public int NumberOfDays { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Leave Duration")]
        public int DurationId { get; set; }

        public SystemCodeDetail? Duration { get; set; }

        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }

        public LeaveType? LeaveType { get; set; }

        public string? Attachment { get; set; }

        public string Description { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        public SystemCodeDetail? Status { get; set; }

        [Display(Name = "Approval Notes")]
        public string? ApprovalNotes { get; set; }

    }
}
