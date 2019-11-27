using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models.View
{
    public class StudentResultView
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select registration number!")]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string RegistrationNumber { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public double Credit { get; set; }
        public int GradeId { get; set; }
        public string GradeLatter { get; set; }
        public decimal GradePoint { get; set; }

    }
}