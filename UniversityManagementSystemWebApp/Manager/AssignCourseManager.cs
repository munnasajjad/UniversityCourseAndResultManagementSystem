using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class AssignCourseManager
    {
        private AssignCourseGataway assignCourseGataway;
        private CourseGateway courseGateway;
        private TeacherGateway teacherGateway;

        public AssignCourseManager()
        {
            assignCourseGataway = new AssignCourseGataway();
            courseGateway = new CourseGateway();
            teacherGateway = new TeacherGateway();
        }

        public bool IsAssignedCourseExist(AssignCourse assignCourse)
        {
            return assignCourseGataway.IsAssignedCourseExist(assignCourse);
        }
        public string Save(AssignCourse assignCourse)
        {
            int assignCourseId = assignCourse.CourseId;
            int assignTeacherId = assignCourse.TeacherId;
            Course course = courseGateway.GetCourseDetailsByCourseId(assignCourseId);
            Teacher teacher = teacherGateway.GetTeacherDetailsById(assignTeacherId);
            double remainingCredit = teacher.RemainingCredit - course.Credit;
            teacher.Id = assignTeacherId;
            teacher.RemainingCredit = remainingCredit;
            int rowAffected = assignCourseGataway.Save(assignCourse);
            if (rowAffected > 0)
            {
                teacherGateway.Update(teacher);
                return "Course assigned!";
            }
            return "Assigning failed!";
        }
        public string UnAssignAllCourse()
        {
            int rowAffected = assignCourseGataway.UnAssignAllCourses();
            if (rowAffected > 0)
            {
                return "All courses are unassigned which are assigned by teachers";
            }
            return "Failed to unassigned courses";
        }
    }
}