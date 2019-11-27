using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class ResultManager
    {
        private ResultGateway resultGateway;

        public ResultManager()
        {
            resultGateway = new ResultGateway();
        }
        public bool IsResultExist(StudentResult studentResult)
        {
            return resultGateway.IsResultExist(studentResult);
        }

        public string Save(StudentResult studentResult)
        {
            int rowAffected = resultGateway.Save(studentResult);
            if (rowAffected > 0)
            {
                return "Result saved successfully!";
            }
            return "Failed to save result!";
        }
        public string UpdateResult(StudentResult studentResult)
        {
            int rowAffected = resultGateway.UpdateResult(studentResult);
            if (rowAffected > 0)
            {
                return "Result updated successfully!";
            }
            return "Failed to update result!";
        }
    }
}