using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class DepartmentManager
    {
        private DepartmentGateway departmentGateway;

        public DepartmentManager()
        {
            departmentGateway = new DepartmentGateway();
        }

        public string Save(Department department)
        {
            int rowAffected = departmentGateway.Save(department);
            if (rowAffected > 0)
            {
                return "Saved successfully!";
            }
            return "Failed to save! Try again.";
        }

        public bool IsDeparmentExist(Department department)
        {
            return departmentGateway.IsDeparmentExist(department);
        }
        public List<Department> GetAllDepartments()
        {
            return departmentGateway.GetAllDepartments();
        }
    }
}