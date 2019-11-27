using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class TeacherController : Controller
    {
        private TeacherManager teacherManager;

        public TeacherController()
        {
            teacherManager = new TeacherManager();
        }
        //
        // GET: /Teacher/
        public ActionResult Save()
        {
            ViewBag.Designations = teacherManager.GetAllDesignations();
            ViewBag.Departments = teacherManager.GetAllDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult Save(Teacher teacher)
        {
            bool isExistTeacher = false;
            if (ModelState.IsValid)
            {
                string message;
                isExistTeacher = teacherManager.IsTeacherExist(teacher.Email);
                if (isExistTeacher)
                {
                    message = "Teacher already exist!";
                    ViewBag.Message = message;
                }
                else
                {
                    message = teacherManager.Save(teacher);
                    ViewBag.Message = message;
                }
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "Model state is invalid!";
            }
            ViewBag.IsTeacherExist = isExistTeacher;
            ViewBag.Designations = teacherManager.GetAllDesignations();
            ViewBag.Departments = teacherManager.GetAllDepartments();
            return View();
        }
    }
}