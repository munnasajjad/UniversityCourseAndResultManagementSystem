using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class DepartmentController : Controller
    {
        private DepartmentManager departmentManager;
        public DepartmentController()
        {
            departmentManager = new DepartmentManager();
        }
        //
        // GET: /Department/
        public ActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(Department department)
        {
            bool isDepartmentExist = false;
            if (ModelState.IsValid)
            {
                string message;
                isDepartmentExist = departmentManager.IsDeparmentExist(department);
                if (isDepartmentExist)
                {
                    message = "The information you entered it's already exist!";
                    ViewBag.Message = message;
                }
                else
                {
                    message = departmentManager.Save(department);
                    ViewBag.Message = message;
                }
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "Model state is not valid";
            }
            ViewBag.IsDepartmentExist = isDepartmentExist;
            return View();
        }

        public ActionResult ViewAllDepartment(Department department)
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }
    }
}