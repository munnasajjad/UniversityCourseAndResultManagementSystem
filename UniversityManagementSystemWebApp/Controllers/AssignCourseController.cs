using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class AssignCourseController : Controller
    {
        private DepartmentManager departmentManager;
        private TeacherManager teacherManager;
        private CourseManager courseManager;
        private AssignCourseManager assignCourseManager;
        private ResultManager resultManager;

        public AssignCourseController()
        {
            departmentManager = new DepartmentManager();
            teacherManager = new TeacherManager();
            courseManager = new CourseManager();
            assignCourseManager = new AssignCourseManager();
            resultManager = new ResultManager();
        }
        //
        // GET: /AssignCourse/
        public ActionResult AssignCourse()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult AssignCourse(AssignCourse assignCourse)
        {
            bool isAssignedCourseExist = false;
            if (ModelState.IsValid)
            {
                string message;
                isAssignedCourseExist = assignCourseManager.IsAssignedCourseExist(assignCourse);
                if (isAssignedCourseExist)
                {
                    message = "Course already assigned!";
                    ViewBag.Message = message;
                }
                else
                {
                    message = assignCourseManager.Save(assignCourse);
                    ViewBag.Message = message;
                }
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "Model state is invalid!";
            }
            ViewBag.IsAssignedCourseExist = isAssignedCourseExist;
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }

        public ActionResult UnAssignCourses()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnAssignCourses(int? a)
        {
            List<Teacher> teachers = teacherManager.GetAllTeachers();
            foreach (Teacher teacher in teachers)
            {
                teacherManager.Update(teacher);
            }
            string messageForUnassigned = assignCourseManager.UnAssignAllCourse();
            string messageForUnenrolled = courseManager.UnEnrollAllCourse();
            string message = messageForUnassigned + " " + messageForUnenrolled;
            ViewBag.Message = message;
            return View();
        }

        public JsonResult GetTeachersByDepartmentId(int? departmentId)
        {
            List<Teacher> teacherList = teacherManager.GetAllTeachersByDepartmentId(departmentId);
            return Json(teacherList);
        }

        public JsonResult GetTeacherDetailsById(int? teacherId)
        {
            var teacher = teacherManager.GetTeacherDetailsById(teacherId);
            return Json(teacher);
        }

        public JsonResult GetAllCoursesByDepartmentId(int? departmentId)
        {
            var courses = courseManager.GetAllCoursesByDepartmentId(departmentId);
            return Json(courses);
        }

        public JsonResult GetCourseDetailsByCourseId(int? courseId)
        {
            var course = courseManager.GetCourseDetailsByCourseId(courseId);
            return Json(course);
        }
    }
}