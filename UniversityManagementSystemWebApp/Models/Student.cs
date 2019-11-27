using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter student name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter an email!")]
        [EmailAddress(ErrorMessage = "Please enter a valid email!")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Please enter contact no.!")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please pick a date!")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Please enter the address!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please select department!")]
        [Display(Name="Department")]
        public int DepartmentId { get; set; }
        public string RegistrationNumber { get; set; }
    }
}