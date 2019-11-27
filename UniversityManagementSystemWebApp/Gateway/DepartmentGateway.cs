using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Gateway
{
    public class DepartmentGateway : BaseGateway
    {
        public int Save(Department department)
        {
            string query = "INSERT INTO Departments (Code, Name) VALUES ('" + department.Code + "','" + department.Name + "')";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();

            return rowAffected;
        }

        public bool IsDeparmentExist(Department department)
        {
            string query = "SELECT Id FROM  Departments WHERE Code='"+department.Code+"' OR Name='"+department.Name+"' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool IsDeparmentExist = Reader.HasRows;
            Connection.Close();
            return IsDeparmentExist;
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
    }
}