using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace UniversityManagementSystemWebApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a course code!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Code must be atleast 5 character long!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please enter the course name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter course credit!")]
        [Range(0.5,5.0, ErrorMessage = "Credit must be in between 0.5 to 5.0!")]
        public double Credit { get; set; }
        //[Required(ErrorMessage = "Please enter the course description!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select a department!")]
        [Display(Name="Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please select a semester!")]
        [Display(Name = "Semester")]
        public int SemesterId { get; set; }

    }
}