using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter teacher name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the address!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter an email!")]
        [EmailAddress(ErrorMessage = "Please enter a valid email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter contact number!")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please select the designation!")]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        [Required(ErrorMessage = "Please select the department!")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please enter the value of credit to be taken!")]
        [Range(1, 50, ErrorMessage = "Credit  to be taken must be a positive number!")]
        [Display(Name = "Credit to be taken")]
        public double CreditTaken { get; set; }
        public double RemainingCredit { get; set; }
    }
}