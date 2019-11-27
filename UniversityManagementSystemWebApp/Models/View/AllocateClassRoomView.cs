using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models.View
{
    public class AllocateClassRoomView
    {
        public int DepartmentId { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public int CourseId { get; set; }
        public int RoomId { get; set; }
        public int DayId { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string RoomNumber { get; set; }
        public string Day { get; set; }
        public string Action { get; set; }

    }
}