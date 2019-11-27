using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class CourseStatisticsController : Controller
    {
        private DepartmentManager departmentManager;
        private CourseManager courseManager;

        public CourseStatisticsController()
        {
            departmentManager = new DepartmentManager();
            courseManager = new CourseManager();
        }
        //
        // GET: /CourseStatistics/
        public ActionResult CourseStatistics()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }
        //[HttpPost]
        //public ActionResult CourseStatistics(int departmentId)
        //{
        //    ViewBag.Departments = departmentManager.GetAllDepartments();
        //    return View();
        //}

        public JsonResult GetCourseStatisticsByDepartmentId(int? departmentId)
        {
            var courseStatics =  courseManager.GetCourseStatisticsByDepartmentId(departmentId);
            return Json(courseStatics);
        }
    }
}