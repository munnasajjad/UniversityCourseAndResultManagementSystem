using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Gateway
{
    public class AssignCourseGataway : BaseGateway
    {
        public bool IsAssignedCourseExist(AssignCourse assignCourse)
        {
            string action = "Assigned";
            string query = "SELECT * FROM AssignCourse WHERE DepartmentId = '" + assignCourse.DepartmentId + "' AND CourseId= '" + assignCourse.CourseId + "' AND Action = '"+action+"' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isAssignedCourse = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return isAssignedCourse;
        }

        public int Save(AssignCourse assignCourse)
        {
            assignCourse.Action = "Assigned";
            string query = "INSERT INTO AssignCourse (DepartmentId, TeacherId, CourseId, Action) VALUES('"+assignCourse.DepartmentId+"', '"+assignCourse.TeacherId+"', '"+assignCourse.CourseId+"', '"+assignCourse.Action+"')";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int UnAssignAllCourses()
        {
            string query = "UPDATE AssignCourse SET Action = 'Unassigned' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}