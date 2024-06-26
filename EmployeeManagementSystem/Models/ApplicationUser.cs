﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? FullName => $"{FirstName} {LastName}";

        public int? EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public DateTime? LoginDate { get; set; }

        public string? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? PasswordChangedOn { get; set; }

        public string? RoleId { get; set; }

        public IdentityRole Role { get; set; }

        public string? Photo { get; set; }

        public string? FileName { get; set; }
        [Display(Name = "Profile Picture")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
