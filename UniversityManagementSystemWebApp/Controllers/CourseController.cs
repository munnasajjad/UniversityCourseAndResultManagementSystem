using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class CourseController : Controller
    {
        private CourseManager courseManager;
        private List<Semester> semesters;
        private List<Department> departments;
        private StudentManager studentManager;

        public CourseController()
        {
            courseManager = new CourseManager();
            semesters = new List<Semester>();
            departments = new List<Department>();
            studentManager = new StudentManager();
        }
        //
        // GET: /Course/
        [HttpGet]
        public ActionResult Save()
        {
            semesters = courseManager.GetAllSemesters();
            departments = courseManager.GetAllDepartments();
            ViewBag.Semesters = semesters;
            ViewBag.Departments = departments;
            return View();
        }
        [HttpPost]
        public ActionResult Save(Course course)
        {
            bool isCourseExist = false;
            if (ModelState.IsValid)
            {
                string message;
                isCourseExist = courseManager.IsCourseExist(course);
                if (isCourseExist)
                {
                    message = "This course is already exists.";
                    ViewBag.Message = message;
                }
                else
                {
                    message = courseManager.Save(course);
                    ViewBag.Message = message;
                }
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "Model state is invalid!";
            }
            semesters = courseManager.GetAllSemesters();
            departments = courseManager.GetAllDepartments();
            ViewBag.Semesters = semesters;
            ViewBag.Departments = departments;
            ViewBag.IsCourseExist = isCourseExist;
            return View();
        }
        [HttpGet]
        public ActionResult EnrollCourse()
        {
            ViewBag.StudentInfo = studentManager.GetStudentInfo();
            return View();
        }
        [HttpPost]
        public ActionResult EnrollCourse(EnrollCourse enrollCourse)
        {
            bool isCourseEnrolled = false;
            if (ModelState.IsValid)
            {
                string message;
                isCourseEnrolled = courseManager.IsCourseEnrolled(enrollCourse);
                if (isCourseEnrolled)
                {
                    message = "Course enrolled before!";
                    ViewBag.Message = message;
                }
                else
                {
                    message = courseManager.SaveEnrolledCourse(enrollCourse);
                    ViewBag.Message = message;
                }
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "Model state is invalid!";
            }
            ViewBag.IsCourseEnrolled = isCourseEnrolled;
            ViewBag.StudentInfo = studentManager.GetStudentInfo();
            return View();
        }
        public JsonResult GetStudentInfoByStudentId(int? studentId)
        {
            var student = studentManager.GetStudentInfoByStudentId(studentId);
            return Json(student);
        }
        public JsonResult GetDepartmentByStudentId(int? studentId)
        {
            var department = studentManager.GetDepartmentByStudentId(studentId);
            return Json(department);
        }
        public JsonResult GetAllCourses(int? studentId)
        {
            Department department = studentManager.GetDepartmentByStudentId(studentId);
            var courses = courseManager.GetAllCoursesByDepartmentId(department.Id);
            return Json(courses);
        }
    }
}