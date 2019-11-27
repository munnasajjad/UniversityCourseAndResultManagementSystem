using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class TeacherManager
    {
        private TeacherGateway teacherGateway;

        public TeacherManager()
        {
            teacherGateway = new TeacherGateway();
        }
        public List<Department> GetAllDepartments()
        {
            return teacherGateway.GetAllDepartments();
        }

        public List<Designation> GetAllDesignations()
        {
            return teacherGateway.GetAllDesignations();
        }

        public bool IsTeacherExist(string email)
        {
            return teacherGateway.IsTeacherExist(email);
        }

        public string Save(Teacher teacher)
        {
            int rowAffected = teacherGateway.Save(teacher);
            if (rowAffected > 0)
            {
                return "Saved successfully!";
            }
            return "Failed to save!";
        }
        public List<Teacher> GetAllTeachersByDepartmentId(int? departmentId)
        {
            return teacherGateway.GetAllTeachersByDepartmentId(departmentId);
        }

        public Teacher GetTeacherDetailsById(int? teacherId)
        {
            return teacherGateway.GetTeacherDetailsById(teacherId);
        }
        public int Update(Teacher teacher)
        {
            return teacherGateway.Update(teacher);
        }
        public List<Teacher> GetAllTeachers()
        {
            return teacherGateway.GetAllTeachers();
        }
    }
}