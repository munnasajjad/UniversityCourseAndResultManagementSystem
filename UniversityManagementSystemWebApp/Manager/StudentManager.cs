using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;
using UniversityManagementSystemWebApp.Models.View;

namespace UniversityManagementSystemWebApp.Manager
{
    public class StudentManager
    {
        private StudentGateway studentGateway;

        public StudentManager()
        {
            studentGateway = new StudentGateway();
        }
        public bool IsStudentExist(string email)
        {
            return studentGateway.IsStudentExist(email);
        }

        public Department GetDepartmentInfoByDepartmentId(int? departmentId)
        {
            return studentGateway.GetDepartmentInfoByDepartmentId(departmentId);
        }

        public string Save(Student student)
        {
            int? departmentId = student.DepartmentId;
            Department department = studentGateway.GetDepartmentInfoByDepartmentId(departmentId);
            string departmentCode = department.Code;
            string year = student.Date.Substring(6, 4);
            string key = departmentCode + "-" + year + "-";
            int totalStudent = studentGateway.CountTotalStudent(key);
            string registrationNumber;
            if (totalStudent >= 0 && totalStudent <= 9)
            {
                registrationNumber = departmentCode + "-" + year + "-00" + (totalStudent + 1);
            }
            else if (totalStudent <= 99 && totalStudent > 9)
            {
                registrationNumber = departmentCode + "-" + year + "-0" + (totalStudent + 1);
            }
            else
            {
                registrationNumber = departmentCode + "-" + year + "-" + (totalStudent + 1);
            }
            student.RegistrationNumber = registrationNumber;
            int rowAffected = studentGateway.Save(student);
            if (rowAffected > 0)
            {
                return "Student registered successfully";
            }
            return "Failed to register student!";
        }

        public Student GetStudent(Student student)
        {
            return student;
        }
        public List<Student> GetStudentInfo()
        {
            return studentGateway.GetStudentInfo();
        }

        public List<Student> GetStudentInfoByEnrollCourses()
        {
            return studentGateway.GetStudentInfoByEnrollCourses();
        }

        public Student GetStudentInfoByStudentId(int? studentId)
        {
            return studentGateway.GetStudentInfoByStudentId(studentId);
        }
        public Department GetDepartmentByStudentId(int? studentId)
        {
            return studentGateway.GetDepartmentByStudentId(studentId);
        }
        public List<Grade> GetAllGrades()
        {
            return studentGateway.GetAllGrades();
        }
        public List<Course> GetAllCoursesEnrolledByStudentId(int? studentId)
        {
            return studentGateway.GetAllCoursesEnrolledByStudentId(studentId);
        }
        public List<StudentResultView> GetStudentResultByStudentId(int? studentId)
        {
            return studentGateway.GetStudentResultByStudentId(studentId);
        }
    }
}