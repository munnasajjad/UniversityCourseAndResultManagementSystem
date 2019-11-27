using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using UniversityManagementSystemWebApp.Models;
using UniversityManagementSystemWebApp.Models.View;

namespace UniversityManagementSystemWebApp.Gateway
{
    public class StudentGateway : BaseGateway
    {
        public bool IsStudentExist(string email)
        {
            string query = "SELECT * FROM Students WHERE Email = '" + email + "' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isStudentExist = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return isStudentExist;
        }

        public Department GetDepartmentInfoByDepartmentId(int? departmentId)
        {
            string query = "SELECT * FROM Departments WHERE Id = '" + departmentId + "' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Department department = new Department();
            while (Reader.Read())
            {
                department.Id = (int)Reader["Id"];
                department.Code = Reader["Code"].ToString();
                department.Name = Reader["Name"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return department;
        }

        public int Save(Student student)
        {
            string query =
                "INSERT INTO Students(Name, Email, ContactNo,Date,Address, DepartmentId,RegistrationNumber) " +
                "VALUES('" + student.Name + "', '" + student.Email + "', '" + student.ContactNo + "','" + student.Date + "', '" + student.Address + "','" + student.DepartmentId + "','" + student.RegistrationNumber + "')";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int CountTotalStudent(string pattern)
        {
            string query = "SELECT COUNT(RegistrationNumber) AS TotalStudent FROM Students WHERE RegistrationNumber LIKE '" + pattern + "%' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int totalStudent = 0;
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                totalStudent = (int)Reader["TotalStudent"];
            }
            Connection.Close();
            return totalStudent;
        }

        public List<Student> GetStudentInfo()
        {
            string query = "SELECT * FROM Students ORDER BY RegistrationNumber ASC";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Student> students = new List<Student>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Student student = new Student
                {
                    Id = (int)Reader["Id"],
                    Name = Reader["Name"].ToString(),
                    Email = Reader["Email"].ToString(),
                    ContactNo = Reader["ContactNo"].ToString(),
                    Date = Reader["Date"].ToString(),
                    DepartmentId = (int)Reader["DepartmentId"],
                    RegistrationNumber = Reader["RegistrationNumber"].ToString()
                };
                students.Add(student);
            }
            Reader.Close();
            Connection.Close();
            return students;
        }

        public List<Student> GetStudentInfoByEnrollCourses()    
        {
            string query = "SELECT * FROM EnrollCourseView WHERE Action = 'Enrolled' ORDER BY Id ASC";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Student> students = new List<Student>();
            Reader = Command.ExecuteReader();
            int id=0;
            while (Reader.Read())
            {
                Student student = new Student
                {
                    Id = (int)Reader["Id"],
                    Name = Reader["Name"].ToString(),
                    Email = Reader["Email"].ToString(),
                    ContactNo = Reader["ContactNo"].ToString(),
                    Date = Reader["Date"].ToString(),
                    DepartmentId = (int)Reader["DepartmentId"],
                    RegistrationNumber = Reader["RegistrationNumber"].ToString()
                };

                if (id != student.Id)
                {
                    students.Add(student);
                    id = student.Id;
                }

                
            }
            Reader.Close();
            Connection.Close();
            return students;
        }

        public Student GetStudentInfoByStudentId(int? studentId)
        {
            string query = "SELECT * FROM Students WHERE Id = '" + studentId + "'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Student student = new Student();
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                student.Id = (int)Reader["Id"];
                student.Name = Reader["Name"].ToString();
                student.Email = Reader["Email"].ToString();
                student.ContactNo = Reader["ContactNo"].ToString();
                student.Date = Reader["Date"].ToString();
                student.Address = Reader["Address"].ToString();
                student.DepartmentId = (int)Reader["DepartmentId"];
                student.RegistrationNumber = Reader["RegistrationNumber"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return student;
        }
        public Department GetDepartmentByStudentId(int? studentId)
        {
            string query = "SELECT * FROM StudentDepartmentView WHERE Id = '" + studentId + "' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Department department = new Department();
            while (Reader.Read())
            {
                department.Id = (int)Reader["DepartmentId"];
                department.Code = Reader["DepartmentCode"].ToString();
                department.Name = Reader["DepartmentName"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return department;
        }

        public List<Grade> GetAllGrades()
        {
            string query = "SELECT * FROM GradeLetter";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Grade> grades = new List<Grade>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Grade grade = new Grade();
                grade.Id = (int)Reader["Id"];
                grade.Name = Reader["Name"].ToString();
                grades.Add(grade);
            }
            Reader.Close();
            Connection.Close();
            return grades;
        }

        public List<Course> GetAllCoursesEnrolledByStudentId(int? studentId)
        {
            string query = "SELECT * FROM StudentCourseView WHERE StudentId = '" + studentId + "' AND Action = 'Enrolled' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Course> courses = new List<Course>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Course course = new Course();
                course.Id = (int)Reader["CourseId"];
                course.Code = Reader["CourseCode"].ToString();
                course.Name = Reader["CourseTitle"].ToString();
                course.DepartmentId = (int)Reader["DepartmentId"];
                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();
            return courses;
        }

        public List<StudentResultView> GetStudentResultByStudentId(int? studentId)
        {
            string query = "SELECT * FROM StudentResultView WHERE StudentId = '" + studentId + "' AND Action = 'Enrolled' ORDER BY CourseCode ASC";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<StudentResultView> studentResultViews = new List<StudentResultView>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                StudentResultView studentResultView = new StudentResultView();
                studentResultView.StudentId = (int)Reader["StudentId"];
                studentResultView.StudentName = Reader["StudentName"].ToString();
                studentResultView.Email = Reader["Email"].ToString();
                studentResultView.RegistrationNumber = Reader["RegistrationNumber"].ToString();
                studentResultView.DepartmentId = (int)Reader["DepartmentId"];
                studentResultView.DepartmentName = Reader["DepartmentName"].ToString();
                studentResultView.CourseId = (int)Reader["CourseId"];
                studentResultView.CourseCode = Reader["CourseCode"].ToString();
                studentResultView.CourseTitle = Reader["CourseTitle"].ToString();
                studentResultView.Credit = (double)Reader["Credit"];
                studentResultView.GradeLatter = Reader["GradeLatter"].ToString();
                if (studentResultView.GradeLatter != "Not Graded Yet")
                {
                    studentResultView.GradeId = (int)Reader["GradeId"];
                }
                studentResultView.GradePoint = (decimal)Reader["GradePoint"];
                studentResultViews.Add(studentResultView);

            }
            Reader.Close();
            Connection.Close();
            return studentResultViews;
        }
    }
}