using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class AssignCourse
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select a department!")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please select a teacher!")]
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Please select a course!")]
        [Display(Name = "Course Code")]
        public int CourseId { get; set; }
        public string Action { get; set; }

    }
}