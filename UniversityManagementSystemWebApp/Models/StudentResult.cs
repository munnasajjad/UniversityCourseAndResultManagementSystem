using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class StudentResult
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select registration number!")]
        [Display(Name = "Student Reg. No.")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please select a course!")]
        [Display(Name = "Select Course")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Choose the grade!")]
        [Display(Name = "Select Grade Latter")]
        public int GradeId { get; set; }
        public string Action { get; set; }

    }
}