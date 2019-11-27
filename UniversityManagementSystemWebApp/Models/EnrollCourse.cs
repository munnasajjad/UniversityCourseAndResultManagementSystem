using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class EnrollCourse
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select registration number!")]
        [Display(Name="Student Reg. No.")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please select a course!")]
        [Display(Name = "Select Course")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Please select date!")]
        public string Date { get; set; }
        public string Action { get; set; }
    }
}