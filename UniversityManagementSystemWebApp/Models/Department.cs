using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter department code!")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Code must be between 2 to 7 characters!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please enter department name!")]
        public string Name { get; set; }
    }
}