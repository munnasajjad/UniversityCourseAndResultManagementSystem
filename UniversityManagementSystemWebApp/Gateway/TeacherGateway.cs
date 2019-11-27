using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Gateway
{
    public class TeacherGateway : BaseGateway
    {
        public List<Designation> GetAllDesignations()
        {
            string query = "SELECT * FROM Designations";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Designation> designations = new List<Designation>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Designation designation = new Designation
                {
                    Id = (int)Reader["Id"],
                    Title = Reader["Title"].ToString()
                };
                designations.Add(designation);
            }
            Reader.Close();
            Connection.Close();
            return designations;
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

        public bool IsTeacherExist(string email)
        {
            string query = "SELECT * FROM Teachers WHERE Email = '" + email + "' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isTeacherExist = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return isTeacherExist;
        }

        public int Save(Teacher teacher)
        {
            string query = "INSERT INTO Teachers (Name, Address, Email, ContactNo, DesignationId, DepartmentId, CreditTaken, RemainingCredit) " +
                           "VALUES('" + teacher.Name + "', '" + teacher.Address + "', '" + teacher.Email + "','" + teacher.ContactNo + "', '" + teacher.DesignationId + "', '" + teacher.DepartmentId + "', " +
                           "'" + teacher.CreditTaken + "', '" + teacher.CreditTaken + "')";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<Teacher> GetAllTeachersByDepartmentId(int? departmentId)
        {
            string query = "SELECT * FROM Teachers WHERE DepartmentId = '" + departmentId + "' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Teacher> teachers = new List<Teacher>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Teacher teacher = new Teacher
                {
                    Id = (int)Reader["Id"],
                    Name = Reader["Name"].ToString()
                };
                teachers.Add(teacher);
            }
            Reader.Close();
            Connection.Close();
            return teachers;
        }

        public Teacher GetTeacherDetailsById(int? teacherId)
        {
            string query = "SELECT * FROM Teachers WHERE Id='" + teacherId + "'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Teacher teacher = new Teacher();
            while (Reader.Read())
            {
                teacher = new Teacher();
                teacher.Id = (int)Reader["Id"];
                teacher.Name = Reader["Name"].ToString();
                teacher.CreditTaken = (double)Reader["CreditTaken"];
                teacher.RemainingCredit = (double)Reader["RemainingCredit"];
            }
            Reader.Close();
            Connection.Close();
            return teacher;
        }

        public int Update(Teacher teacher)
        {
            string query = "UPDATE Teachers SET RemainingCredit = " + teacher.RemainingCredit + " WHERE Id=" + teacher.Id + "";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public List<Teacher> GetAllTeachers()
        {
            string query = "SELECT * FROM Teachers";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Teacher> teachers = new List<Teacher>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Teacher teacher = new Teacher();
                teacher.Id = (int)Reader["Id"];
                teacher.Name = Reader["Name"].ToString();
                teacher.CreditTaken = (double)Reader["CreditTaken"];
                teacher.RemainingCredit = teacher.CreditTaken;
                teachers.Add(teacher);
            }
            Reader.Close();
            Connection.Close();
            return teachers;
        }
    }
}