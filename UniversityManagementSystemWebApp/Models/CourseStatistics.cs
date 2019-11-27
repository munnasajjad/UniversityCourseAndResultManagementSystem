using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class CourseStatistics
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CourseTitle { get; set; }
        public string Semester { get; set; }
        public string TeacherName { get; set; }
        [Required(ErrorMessage = "Please select a department!")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public string Action { get; set; }
    }
}