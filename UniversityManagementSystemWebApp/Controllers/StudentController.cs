using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class StudentController : Controller
    {
        private DepartmentManager departmentManager;
        private StudentManager studentManager;
        private ResultManager resultManager;

        public StudentController()
        {
            departmentManager = new DepartmentManager();
            studentManager = new StudentManager();
            resultManager = new ResultManager();
        }
        //
        // GET: /Student/
        [HttpGet]
        public ActionResult SaveStudent()
        {
            string currentDate = DateTime.Today.ToString("dd/MM/yyyy");
            ViewBag.CurrentDate = currentDate;
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult SaveStudent(Student student)
        {
            bool isStudentExist = false;
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                string message;
                isStudentExist = studentManager.IsStudentExist(student.Email);
                if (isStudentExist)
                {
                    message = "Student already exist!";
                    ViewBag.Message = message;
                }
                else
                {
                    message = studentManager.Save(student);
                    ViewBag.StudentInfo = studentManager.GetStudent(student);
                    Department department = studentManager.GetDepartmentInfoByDepartmentId(student.DepartmentId);
                    ViewBag.DepartmentName = department.Name;
                    ViewBag.Message = message;
                }
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "Model state is invalid!";
            }
            ViewBag.CurrentDate = DateTime.Today.ToString("dd/MM/yyyy");
            ViewBag.Departments = departmentManager.GetAllDepartments();
            ViewBag.IsStudentExist = isStudentExist;
            return View();
        }
        [HttpGet]
        public ActionResult StudentResult()
        {
            ViewBag.Grades = studentManager.GetAllGrades();
            ViewBag.StudentInfo = studentManager.GetStudentInfo();
            return View();
        }
        [HttpPost]
        public ActionResult StudentResult(StudentResult studentResult)
        {
            bool isResultExist = false;
            if (ModelState.IsValid)
            {
                string message;
                isResultExist = resultManager.IsResultExist(studentResult);
                if (isResultExist)
                {

                    message = resultManager.UpdateResult(studentResult);
                    ViewBag.Message = message;
                }
                else
                {
                    message = resultManager.Save(studentResult);
                    ViewBag.Message = message;
                }
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "Model state is invalid!";
            }
            ViewBag.IsResultExist = isResultExist;
            ViewBag.Grades = studentManager.GetAllGrades();
            ViewBag.StudentInfo = studentManager.GetStudentInfo();
            return View();
        }
        [HttpGet]
        public ActionResult ViewResult()
        {
            List<Student> students = studentManager.GetStudentInfoByEnrollCourses();
            ViewBag.StudentInfo = students;
            return View();
        }
        [HttpPost]
        public ActionResult ViewResult(StudentResult studentResult)
        {
            ViewBag.StudentInfo = studentManager.GetStudentInfoByEnrollCourses();
            return View();
        }

        public ActionResult CreateResult(StudentResult studentResult)
        {
            var studentInfo = studentManager.GetStudentInfoByStudentId(studentResult.StudentId);
            var department = studentManager.GetDepartmentByStudentId(studentResult.StudentId);
            var result = studentManager.GetStudentResultByStudentId(studentResult.StudentId);
            if (result.Count == 0)
            {
                ViewBag.Message = "No result found!";
            }
            else
            {
                ViewBag.StudentInfo = studentInfo;
                ViewBag.Department = department;
                ViewBag.Result = result;
                double totalCredit = 0.00d, totalPoint = 0.00d, point;
                foreach (var value in result)
                {
                    totalCredit += value.Credit;
                    point = Convert.ToDouble(value.GradePoint);
                    totalPoint += point * value.Credit;
                }
                string cgpa = Convert.ToString(totalPoint / totalCredit);
                if (cgpa.Length < 4)
                {
                    ViewBag.CGPA = cgpa + ".00";
                }
                else
                {
                    ViewBag.CGPA = cgpa.Substring(0, 4);
                }

            }
            return View();
        }
        public ActionResult ResultSheet(StudentResult studentResult)
        {
            return new ActionAsPdf("CreateResult", studentResult);
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
        public JsonResult GetAllCoursesEnrolledByStudentId(int? studentId)
        {
            var courses = studentManager.GetAllCoursesEnrolledByStudentId(studentId);
            return Json(courses);
        }
        public JsonResult GetStudentResultByStudentId(int? studentId)
        {
            var studentResult = studentManager.GetStudentResultByStudentId(studentId);
            return Json(studentResult);
        }
    }
}