using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class CourseManager
    {
        private CourseGateway courseGateway;

        public CourseManager()
        {
            courseGateway = new CourseGateway();
        }
        public List<Semester> GetAllSemesters()
        {
            return courseGateway.GetAllSemesters();
        }

        public List<Department> GetAllDepartments()
        {
            return courseGateway.GetAllDepartments();
        }

        public string Save(Course course)
        {
            int rowAffected = courseGateway.Save(course);
            if (rowAffected > 0)
            {
                return "Saved successfully";
            }
            return "Falied to save";
        }

        public bool IsCourseExist(Course course)
        {
            return courseGateway.IsCourseExist(course);
        }
        public List<Course> GetAllCoursesByDepartmentId(int? departmentId)
        {
            return courseGateway.GetAllCoursesByDepartmentId(departmentId);
        }
        public Course GetCourseDetailsByCourseId(int? courseId)
        {
            return courseGateway.GetCourseDetailsByCourseId(courseId);
        }
        public List<CourseStatistics> GetCourseStatisticsByDepartmentId(int? departmentId)
        {
            return courseGateway.GetCourseStatisticsByDepartmentId(departmentId);
        }
        public string SaveEnrolledCourse(EnrollCourse enrollCourse)
        {
            int rowAffected = courseGateway.SaveEnrolledCourse(enrollCourse);
            if (rowAffected > 0)
            {
                return "Course enrolled!";
            }
            return "Failed to enroll course";
        }

        public bool IsCourseEnrolled(EnrollCourse enrollCourse)
        {
            return courseGateway.IsCourseEnrolled(enrollCourse);
        }
        public string UnEnrollAllCourse()
        {
            int rowAffected = courseGateway.UnEnrollAllCourse();
            if (rowAffected > 0)
            {
                return "All courses are unenrolled which are enrolled by students";
            }
            return "Failed to unenrolled courses";
        }
    }
}