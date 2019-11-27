using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class AllocateClassRoom
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select department!")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please select course!")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Please select room no.!")]
        [Display(Name = "Room No.")]
        public int RoomId { get; set; }
        [Required(ErrorMessage = "Please select day!")]
        [Display(Name = "Day")]
        public int DayId { get; set; }
        [Required(ErrorMessage = "Please select time to start from!")]
        [Display(Name = "From")]
        public string FromTime { get; set; }
        [Required(ErrorMessage = "Please select end time!")]
        [Display(Name = "To")]
        public string ToTime { get; set; }

        public string Action { get; set; }
    }
}