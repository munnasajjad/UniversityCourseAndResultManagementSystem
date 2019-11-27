using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Gateway
{
    public class ResultGateway : BaseGateway
    {
        public bool IsResultExist(StudentResult studentResult)
        {
            string query = "SELECT * FROM Result WHERE StudentId ='"+studentResult.StudentId+"' AND CourseId ='"+studentResult.CourseId+"' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isResultExist = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return isResultExist;
        }

        public int Save(StudentResult studentResult)
        {
            string query = "INSERT INTO Result(StudentId, CourseId, GradeId, Action) " +
                           "VALUES('" + studentResult.StudentId + "','" + studentResult.CourseId + "', '" + studentResult.GradeId + "','" + studentResult.Action + "')";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int UpdateResult(StudentResult studentResult)
        {
            string query = "UPDATE Result SET GradeId = '"+studentResult.GradeId+"' WHERE StudentId = '"+studentResult.StudentId+"' AND CourseId = '"+studentResult.CourseId+"'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}