using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Gateway
{
    public class CourseGateway : BaseGateway
    {
        public List<Semester> GetAllSemesters()
        {
            string query = "SELECT * FROM Semesters";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Semester> semesters = new List<Semester>();
            //semesters.Add(new Semester{Id=0, Name="---Select a semester---"});
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Semester semester = new Semester
                {
                    Id = (int)Reader["Id"],
                    Name = Reader["Name"].ToString()
                };

                semesters.Add(semester);
            }
            Reader.Close();
            Connection.Close();
            return semesters;
        }

        public List<Department> GetAllDepartments()
        {
            string query = "SELECT * FROM Departments ORDER BY Name ASC";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Department> departments = new List<Department>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Department department = new Department
                {
                    Id = (int)Reader["Id"],
                    Code = Reader["Code"].ToString(),
                    Name = Reader["Name"].ToString()
                };

                departments.Add(department);
            }
            Reader.Close();
            Connection.Close();
            return departments;
        }

        public int Save(Course course)
        {
            string query = "INSERT INTO Courses (Code, Name, Credit, Description, DepartmentId, SemesterId) " +
                           "VALUES ('" + course.Code + "', '" + course.Name + "','" + course.Credit + "','" + course.Description + "','" + course.DepartmentId + "','" + course.SemesterId + "')";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool IsCourseExist(Course course)
        {
            string query = "SELECT Id FROM  Courses WHERE Code='" + course.Code + "' OR Name='" + course.Name + "' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool IsDeparmentExist = Reader.HasRows;
            Connection.Close();
            return IsDeparmentExist;
        }

        public List<Course> GetAllCoursesByDepartmentId(int? departmentId)
        {
            string query = "SELECT * FROM Courses WHERE DepartmentId='" + departmentId + "'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Course> courses = new List<Course>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Course course = new Course();
                course.Id = (int)Reader["Id"];
                course.Code = Reader["Code"].ToString();
                course.Name = Reader["Name"].ToString();
                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();
            return courses;
        }

        public Course GetCourseDetailsByCourseId(int? courseId)
        {
            string query = "SELECT * FROM Courses WHERE Id='" + courseId + "'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Course course = new Course();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                course = new Course
                {
                    Id = (int)Reader["Id"],
                    Code = Reader["Code"].ToString(),
                    Name = Reader["Name"].ToString(),
                    Credit = Convert.ToDouble(Reader["Credit"])
                };
            }
            Reader.Close();
            Connection.Close();
            return course;
        }

        public List<CourseStatistics> GetCourseStatisticsByDepartmentId(int? departmentId)
        {
            string query = "SELECT * FROM CourseStatisticsView WHERE DepartmentId = '" + departmentId + "' ORDER BY Code ASC";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<CourseStatistics> courseStatisticses = new List<CourseStatistics>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                CourseStatistics courseStatistics = new CourseStatistics();
                courseStatistics.Code = Reader["Code"].ToString();
                courseStatistics.CourseTitle = Reader["CourseTitle"].ToString();
                courseStatistics.Semester = Reader["Semester"].ToString();
                courseStatistics.TeacherName = Reader["TeacherName"].ToString();
                courseStatistics.Action = Reader["Action"].ToString();
                if (courseStatistics.Action != "Not Assigned Yet")
                {
                    courseStatisticses.Add(courseStatistics);
                }

            }
            Reader.Close();
            Connection.Close();
            return courseStatisticses;
        }

        public int SaveEnrolledCourse(EnrollCourse enrollCourse)
        {
            enrollCourse.Action = "Enrolled";
            string query = "INSERT INTO EnrollCourse (StudentId, CourseId, Date, Action) " +
                       "VALUES ('" + enrollCourse.StudentId + "', '" + enrollCourse.CourseId + "','" + enrollCourse.Date + "','" + enrollCourse.Action + "')";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool IsCourseEnrolled(EnrollCourse enrollCourse)
        {
            string query = "SELECT * FROM EnrollCourse WHERE StudentId='" + enrollCourse.StudentId + "' AND CourseId='" + enrollCourse.CourseId + "' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isCourseEnrolled = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return isCourseEnrolled;
        }
        public int UnEnrollAllCourse()
        {
           string query = "UPDATE EnrollCourse SET Action = 'Unenrolled' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}